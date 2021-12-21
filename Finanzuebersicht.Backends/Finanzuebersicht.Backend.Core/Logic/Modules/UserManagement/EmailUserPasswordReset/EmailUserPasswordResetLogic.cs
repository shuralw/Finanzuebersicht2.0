﻿using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.UserManagement.EmailUserPasswordReset;
using Finanzuebersicht.Backend.Core.Contract.Logic.SystemConnections.Email;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Identifier;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Password;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Time;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.UserManagement.EmailUserPasswortResetTokens;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.UserManagement.EmailUsers;
using Finanzuebersicht.Backend.Core.Logic.LogicResults;
using Finanzuebersicht.Backend.Core.Logic.SystemConnections.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Finanzuebersicht.Backend.Core.Logic.Modules.UserManagement.EmailUserPasswordReset
{
    internal class EmailUserPasswordResetLogic : IEmailUserPasswordResetLogic
    {
        private readonly IEmailUserPasswortResetTokensRepository emailUserPasswordResetTokenRepository;
        private readonly IEmailUsersRepository emailUsersRepository;

        private readonly IEmailClient emailClient;
        private readonly IPasswordHasher passwordHasher;
        private readonly ISHA256TokenGenerator sha256TokenGenerator;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly ILogger<EmailUserPasswordResetLogic> logger;

        private readonly EmailUserPasswordResetOptions options;

        private readonly EmailUserPasswordResetMail mail;

        public EmailUserPasswordResetLogic(
            IEmailUserPasswortResetTokensRepository emailUserPasswordResetTokenRepository,
            IEmailUsersRepository emailUsersRepository,
            IEmailClient emailClient,
            IPasswordHasher passwordHasher,
            ISHA256TokenGenerator sha256TokenGenerator,
            IDateTimeProvider dateTimeProvider,
            ILogger<EmailUserPasswordResetLogic> logger,
            IOptions<EmailUserPasswordResetOptions> options)
        {
            this.emailUserPasswordResetTokenRepository = emailUserPasswordResetTokenRepository;
            this.emailUsersRepository = emailUsersRepository;
            this.emailClient = emailClient;
            this.passwordHasher = passwordHasher;
            this.sha256TokenGenerator = sha256TokenGenerator;
            this.dateTimeProvider = dateTimeProvider;

            this.logger = logger;

            this.options = options.Value;

            this.mail = new EmailUserPasswordResetMail();
        }

        public ILogicResult InitializePasswordReset(string email, IBrowserInfo browserInfo)
        {
            var emailUser = this.emailUsersRepository.GetEmailUser(email);

            if (emailUser == null)
            {
                this.logger.LogDebug("EmailUser konnte nicht gefunden werden.");
                return LogicResult.NotFound("EmailUser konnte nicht gefunden werden.");
            }

            DbEmailUserPasswordResetToken token = this.CreateToken(emailUser);

            this.emailUserPasswordResetTokenRepository.CreateToken(token);

            this.SendPasswordResetMail(email, token, browserInfo);

            this.logger.LogInformation("Passwort-Reset wurde gestartet und eine Mail an {email} versendet", email);
            return LogicResult.Ok();
        }

        public ILogicResult ResetPassword(string emailUserPasswordResetToken, string newPassword)
        {
            var emailUserPasswordResetTokenInfo = this.emailUserPasswordResetTokenRepository.GetToken(emailUserPasswordResetToken);

            if (emailUserPasswordResetTokenInfo == null || emailUserPasswordResetTokenInfo.ExpiresOn <= this.dateTimeProvider.Now())
            {
                this.logger.LogDebug("EmailUserPasswordResetToken konnte nicht gefunden werden.");
                return LogicResult.NotFound("EmailUserPasswordResetToken konnte nicht gefunden werden.");
            }

            this.emailUserPasswordResetTokenRepository.DeleteToken(emailUserPasswordResetToken);

            IDbEmailUser emailUser = this.emailUsersRepository.GetEmailUser(emailUserPasswordResetTokenInfo.EmailUserId);
            this.UpdateEmailUserWithNewPassword(emailUser, newPassword);

            this.logger.LogInformation("Der EmailUser {email} hat sein Passwort über die Passwort-Zurücksetzen-Funktion neu vergeben", emailUser.Email);
            return LogicResult.Ok();
        }

        public void RemoveExpiredPasswordResetTokens()
        {
            this.emailUserPasswordResetTokenRepository.DeleteToken(this.dateTimeProvider.Now());
        }

        private DbEmailUserPasswordResetToken CreateToken(IDbEmailUser emailUser)
        {
            return new DbEmailUserPasswordResetToken()
            {
                Token = this.sha256TokenGenerator.Generate(),
                ExpiresOn = this.dateTimeProvider.Now().AddMinutes(this.options.ExpirationTimeInMinutes),
                EmailUserId = emailUser.Id
            };
        }

        private void SendPasswordResetMail(string email, DbEmailUserPasswordResetToken emailUserPasswordResetToken, IBrowserInfo browserInfo)
        {
            string mailMessage = this.mail.GetMessage(new EmailUserPasswordResetMailMetadata()
            {
                MailResetPasswordUrlPrefix = this.options.MailResetPasswordUrlPrefix,
                MailHomepageUrl = this.options.MailHomepageUrl,
                MailSupportUrl = this.options.MailSupportUrl,
                EmailUserEmail = email,
                EmailUserPasswordResetToken = emailUserPasswordResetToken.Token,
                EmailUserBrowserInfo = browserInfo
            });

            this.emailClient.Send(new Email()
            {
                To = email,
                Subject = "[[Produktname]] Passwort zurücksetzen",
                Message = mailMessage
            });
        }

        private void UpdateEmailUserWithNewPassword(IDbEmailUser emailUser, string newPassword)
        {
            IPasswordHash hash = this.passwordHasher.HashPassword(newPassword);
            emailUser.PasswordHash = hash.Hash;
            emailUser.PasswordSalt = hash.Salt;
            this.emailUsersRepository.UpdateEmailUser(emailUser);
        }
    }
}