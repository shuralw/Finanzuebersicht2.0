using Contract.Architecture.Contract;
using Contract.Architecture.Contract.Logic.LogicResults;
using Contract.Architecture.Contract.Logic.Model.Users.EmailUsers;
using Contract.Architecture.Contract.Logic.Services.Password;
using Contract.Architecture.Contract.Persistence.Model.Users.EmailUsers;
using Contract.Architecture.Logic.LogicResults;
using Microsoft.Extensions.Logging;

namespace Contract.Architecture.Logic.Model.Users.EmailUsers
{
    internal class EmailUserPasswordChangeLogic : IEmailUserPasswordChangeLogic
    {
        private readonly IEmailUsersRepository emailUsersRepository;

        private readonly IBsiPasswordService bsiPasswordService;
        private readonly ISessionContext sessionContext;
        private readonly ILogger<EmailUserPasswordChangeLogic> logger;

        public EmailUserPasswordChangeLogic(
            IEmailUsersRepository emailUsersRepository,
            IBsiPasswordService bsiPasswordService,
            ISessionContext sessionContext,
            ILogger<EmailUserPasswordChangeLogic> logger)
        {
            this.emailUsersRepository = emailUsersRepository;

            this.bsiPasswordService = bsiPasswordService;
            this.sessionContext = sessionContext;
            this.logger = logger;
        }

        public ILogicResult ChangePassword(string oldPassword, string newPassword)
        {
            IDbEmailUser emailUser = this.emailUsersRepository.GetEmailUser(this.sessionContext.EmailUserId);
            if (!this.IsPasswordValid(oldPassword, emailUser))
            {
                this.logger.LogWarning("Das alte Passwort ist falsch.");
                return LogicResult.Forbidden();
            }

            this.UpdateEmailUserWithNewPassword(emailUser, newPassword);

            this.logger.LogInformation("Das Passwort wurde mit der Passwort-Ändern-Funktion neu vergeben.");
            return LogicResult.Ok();
        }

        private void UpdateEmailUserWithNewPassword(IDbEmailUser emailUser, string newPassword)
        {
            IBSIPasswordHash hash = this.bsiPasswordService.HashPassword(newPassword);
            emailUser.PasswordHash = hash.PasswordHash;
            emailUser.PasswordSalt = hash.Salt;
            this.emailUsersRepository.UpdateEmailUser(emailUser);
        }

        private bool IsPasswordValid(string password, IDbEmailUser emailUser)
        {
            return this.bsiPasswordService.ComparePasswords(password, emailUser.PasswordHash, emailUser.PasswordSalt);
        }
    }
}