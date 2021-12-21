﻿using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.UserManagement.EmailUserPasswordReset;
using Finanzuebersicht.Backend.Core.Contract.Logic.SystemConnections.Email;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Identifier;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Password;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Time;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.UserManagement.EmailUserPasswortResetTokens;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.UserManagement.EmailUsers;
using Finanzuebersicht.Backend.Core.Logic.Modules.UserManagement.EmailUserPasswordReset;
using Finanzuebersicht.Backend.Core.Logic.Modules.UserManagement.EmailUsers;
using Finanzuebersicht.Backend.Core.Logic.Tools.Password;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Finanzuebersicht.Backend.Core.Logic.Tests.Modules.UserManagement.EmailUserPasswordReset
{
    [TestClass]
    public class EmailUserPasswordResetLogicTests
    {
        private static readonly string Browser = "Chrome";
        private static readonly string Email = "test@example.org";
        private static readonly Guid EmailUserId = Guid.Parse("59f7717b-4647-4d33-9da4-b2a6a7d126c5");
        private static readonly int ExpirationTimeInMinutes = 30;
        private static readonly DateTime Expired = new DateTime(2020, 1, 1, 11, 30, 0);
        private static readonly DateTime ExpiresOn = new DateTime(2020, 1, 1, 12, 30, 0);
        private static readonly string MailHomepageUrl = "https://example.org";
        private static readonly string MailResetPasswordUrlPrefix = "https://example.org/login/reset-password";
        private static readonly string MailSupportUrl = "support@example.org";
        private static readonly DateTime Now = new DateTime(2020, 1, 1, 12, 0, 0);
        private static readonly string OperatingSystem = "Windows 10";
        private static readonly string Password = "123QWEasd!";
        private static readonly string PasswordHash = "lLj3sQPf1isP6T1CZWZ9RMN3W9okAdTk4OjooKHO+9BT5tJ55euCLde8ifSl6ru6SuaypWRiE1nkiZPNHDbu4A==";
        private static readonly string PasswordSalt = "50000.voYJdI+L2w/atDbVrWlMRUw8MkmXeBO9c35Ms2wQZfYQkw==";
        private static readonly bool RunOnInitialization = true;
        private static readonly string Token = "8c176b78e9e55db969bfc3fa67fcc3eff4434bd27883b47a52af7185b0710393";

        [TestMethod]
        public void InitializePasswordResetNotFoundTest()
        {
            // Arrange
            Mock<IEmailUsersRepository> emailUsersRepository = this.SetupEmailUsersRepositoryNotExisting();

            IBrowserInfo browserInfo = this.CreateBrowserInfo();

            EmailUserPasswordResetLogic emailUserPasswordResetLogic = new EmailUserPasswordResetLogic(
                null,
                emailUsersRepository.Object,
                null,
                null,
                null,
                null,
                Mock.Of<ILogger<EmailUserPasswordResetLogic>>(),
                this.SetupOptions());

            // Act
            var result = emailUserPasswordResetLogic.InitializePasswordReset(Email, browserInfo);

            // Assert
            Assert.AreEqual(LogicResultState.NotFound, result.State);
        }

        [TestMethod]
        public void InitializePasswordResetTest()
        {
            // Arrange
            Mock<IEmailUsersRepository> emailUsersRepository = this.SetupEmailUsersRepositoryExisting();
            Mock<IEmailUserPasswortResetTokensRepository> emailUserPasswordResetTokenRepository = this.SetupEmailUserPasswordResetTokenRepositoryDefault();
            Mock<IEmailClient> emailClient = this.SetupEmailClientDefault();
            Mock<ISHA256TokenGenerator> sha256TokenGenerator = this.SetupSHA256TokenGeneratorDefault();
            Mock<IDateTimeProvider> dateTimeProvider = this.SetupDateTimeProviderDefault();

            IBrowserInfo browserInfo = this.CreateBrowserInfo();

            EmailUserPasswordResetLogic emailUserPasswordResetLogic = new EmailUserPasswordResetLogic(
                emailUserPasswordResetTokenRepository.Object,
                emailUsersRepository.Object,
                emailClient.Object,
                null,
                sha256TokenGenerator.Object,
                dateTimeProvider.Object,
                Mock.Of<ILogger<EmailUserPasswordResetLogic>>(),
                this.SetupOptions());

            // Act
            var result = emailUserPasswordResetLogic.InitializePasswordReset(Email, browserInfo);

            // Assert
            Assert.AreEqual(LogicResultState.Ok, result.State);
        }

        [TestMethod]
        public void RemoveExpiredPasswordResetTokensTest()
        {
            // Arrange
            Mock<IEmailUserPasswortResetTokensRepository> emailUserPasswordResetTokenRepository = this.SetupEmailUserPasswordResetTokenRepositoryDefault();
            Mock<IDateTimeProvider> dateTimeProvider = this.SetupDateTimeProviderDefault();

            EmailUserPasswordResetLogic emailUserPasswordResetLogic = new EmailUserPasswordResetLogic(
                emailUserPasswordResetTokenRepository.Object,
                null,
                null,
                null,
                null,
                dateTimeProvider.Object,
                Mock.Of<ILogger<EmailUserPasswordResetLogic>>(),
                this.SetupOptions());

            // Act
            emailUserPasswordResetLogic.RemoveExpiredPasswordResetTokens();
            emailUserPasswordResetTokenRepository.Verify(repository => repository.DeleteToken(Now), Times.Once);
        }

        [TestMethod]
        public void ResetPasswordNotFoundExpiredTest()
        {
            // Arrange
            Mock<IEmailUserPasswortResetTokensRepository> emailUserPasswordResetTokenRepository = this.SetupEmailUserPasswordResetTokenRepositoryExpired();
            Mock<IDateTimeProvider> dateTimeProvider = this.SetupDateTimeProviderDefault();

            EmailUserPasswordResetLogic emailUserPasswordResetLogic = new EmailUserPasswordResetLogic(
                emailUserPasswordResetTokenRepository.Object,
                null,
                null,
                null,
                null,
                dateTimeProvider.Object,
                Mock.Of<ILogger<EmailUserPasswordResetLogic>>(),
                this.SetupOptions());

            // Act
            var result = emailUserPasswordResetLogic.ResetPassword(Token, Password);

            // Assert
            Assert.AreEqual(LogicResultState.NotFound, result.State);
        }

        [TestMethod]
        public void ResetPasswordNotFoundTokenTest()
        {
            // Arrange
            Mock<IEmailUserPasswortResetTokensRepository> emailUserPasswordResetTokenRepository = this.SetupEmailUserPasswordResetTokenRepositoryNotFound();

            EmailUserPasswordResetLogic emailUserPasswordResetLogic = new EmailUserPasswordResetLogic(
                emailUserPasswordResetTokenRepository.Object,
                null,
                null,
                null,
                null,
                null,
                Mock.Of<ILogger<EmailUserPasswordResetLogic>>(),
                this.SetupOptions());

            // Act
            var result = emailUserPasswordResetLogic.ResetPassword(Token, Password);

            // Assert
            Assert.AreEqual(LogicResultState.NotFound, result.State);
        }

        [TestMethod]
        public void ResetPasswordTest()
        {
            // Arrange
            Mock<IEmailUsersRepository> emailUsersRepository = this.SetupEmailUsersRepositoryExisting();
            Mock<IEmailUserPasswortResetTokensRepository> emailUserPasswordResetTokenRepository = this.SetupEmailUserPasswordResetTokenRepositoryDefault();
            Mock<IPasswordHasher> passwordHasher = this.SetupPasswordHasherDefault();
            Mock<IDateTimeProvider> dateTimeProvider = this.SetupDateTimeProviderDefault();

            EmailUserPasswordResetLogic emailUserPasswordResetLogic = new EmailUserPasswordResetLogic(
                emailUserPasswordResetTokenRepository.Object,
                emailUsersRepository.Object,
                null,
                passwordHasher.Object,
                null,
                dateTimeProvider.Object,
                Mock.Of<ILogger<EmailUserPasswordResetLogic>>(),
                this.SetupOptions());

            // Act
            var result = emailUserPasswordResetLogic.ResetPassword(Token, Password);

            // Assert
            Assert.AreEqual(LogicResultState.Ok, result.State);
            emailUserPasswordResetTokenRepository.Verify(repository => repository.DeleteToken(Token), Times.Once);
        }

        private IBrowserInfo CreateBrowserInfo()
        {
            Mock<IBrowserInfo> browserInfoMock = new Mock<IBrowserInfo>(MockBehavior.Strict);
            browserInfoMock.Setup(browserInfo => browserInfo.Browser).Returns(Browser);
            browserInfoMock.Setup(browserInfo => browserInfo.OperatingSystem).Returns(OperatingSystem);
            var browserInfo = browserInfoMock.Object;
            return browserInfo;
        }

        private DbEmailUser CreateDbEmailUser()
        {
            return new DbEmailUser()
            {
                Id = EmailUserId,
                Email = Email,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt,
            };
        }

        private Mock<IPasswordHasher> SetupPasswordHasherDefault()
        {
            var passwordHasher = new Mock<IPasswordHasher>(MockBehavior.Strict);
            passwordHasher.Setup(service => service.HashPassword(Password))
                .Returns(new PasswordHash()
                {
                    Hash = PasswordHash,
                    Salt = PasswordSalt
                });
            return passwordHasher;
        }

        private Mock<IDateTimeProvider> SetupDateTimeProviderDefault()
        {
            Mock<IDateTimeProvider> dateTimeProvider = new Mock<IDateTimeProvider>(MockBehavior.Strict);
            dateTimeProvider.Setup(service => service.Now()).Returns(() => Now);
            return dateTimeProvider;
        }

        private Mock<IEmailClient> SetupEmailClientDefault()
        {
            Mock<IEmailClient> emailClient = new Mock<IEmailClient>(MockBehavior.Strict);
            emailClient.Setup(service => service.Send(It.IsAny<IEmail>())).Callback((IEmail email) =>
            {
                Assert.AreEqual(Email, email.To);
                Assert.IsNotNull(email.Subject);
                Assert.IsNotNull(email.Message);
            });
            return emailClient;
        }

        private Mock<IEmailUserPasswortResetTokensRepository> SetupEmailUserPasswordResetTokenRepositoryDefault()
        {
            Mock<IEmailUserPasswortResetTokensRepository> emailUserPasswordResetTokenRepository = new Mock<IEmailUserPasswortResetTokensRepository>(MockBehavior.Strict);
            emailUserPasswordResetTokenRepository.Setup(repository => repository.CreateToken(It.IsAny<IDbEmailUserPasswordResetToken>()))
                .Callback((IDbEmailUserPasswordResetToken token) =>
                {
                    Assert.AreEqual(Token, token.Token);
                    Assert.AreEqual(EmailUserId, token.EmailUserId);
                    Assert.AreEqual(ExpiresOn, token.ExpiresOn);
                });
            emailUserPasswordResetTokenRepository.Setup(repository => repository.DeleteToken(Token));
            emailUserPasswordResetTokenRepository.Setup(repository => repository.DeleteToken(Now));
            emailUserPasswordResetTokenRepository.Setup(repository => repository.GetToken(Token))
                .Returns(new DbEmailUserPasswordResetToken()
                {
                    Token = Token,
                    ExpiresOn = ExpiresOn,
                    EmailUserId = EmailUserId
                });
            return emailUserPasswordResetTokenRepository;
        }

        private Mock<IEmailUserPasswortResetTokensRepository> SetupEmailUserPasswordResetTokenRepositoryExpired()
        {
            Mock<IEmailUserPasswortResetTokensRepository> emailUserPasswordResetTokenRepository = new Mock<IEmailUserPasswortResetTokensRepository>(MockBehavior.Strict);
            emailUserPasswordResetTokenRepository.Setup(repository => repository.GetToken(It.IsAny<string>()))
                .Returns((string token) =>
                {
                    Assert.AreEqual(Token, token);
                    return new DbEmailUserPasswordResetToken()
                    {
                        Token = Token,
                        ExpiresOn = Expired,
                        EmailUserId = EmailUserId
                    };
                });
            return emailUserPasswordResetTokenRepository;
        }

        private Mock<IEmailUserPasswortResetTokensRepository> SetupEmailUserPasswordResetTokenRepositoryNotFound()
        {
            Mock<IEmailUserPasswortResetTokensRepository> emailUserPasswordResetTokenRepository = new Mock<IEmailUserPasswortResetTokensRepository>(MockBehavior.Strict);
            emailUserPasswordResetTokenRepository.Setup(repository => repository.GetToken(Token)).Returns(() => null);
            return emailUserPasswordResetTokenRepository;
        }

        private Mock<IEmailUsersRepository> SetupEmailUsersRepositoryExisting()
        {
            var emailUsersRepository = new Mock<IEmailUsersRepository>(MockBehavior.Strict);
            emailUsersRepository.Setup(repository => repository.GetEmailUser(EmailUserId)).Returns(this.CreateDbEmailUser());
            emailUsersRepository.Setup(repository => repository.GetEmailUser(Email)).Returns(this.CreateDbEmailUser());
            emailUsersRepository.Setup(repository => repository.UpdateEmailUser(It.IsAny<IDbEmailUser>())).Callback((IDbEmailUser dbEmailUser) =>
            {
                Assert.AreEqual(EmailUserId, dbEmailUser.Id);
                Assert.AreEqual(Email, dbEmailUser.Email);
                Assert.AreEqual(PasswordHash, dbEmailUser.PasswordHash);
                Assert.AreEqual(PasswordSalt, dbEmailUser.PasswordSalt);
            });
            return emailUsersRepository;
        }

        private Mock<IEmailUsersRepository> SetupEmailUsersRepositoryNotExisting()
        {
            var emailUsersRepository = new Mock<IEmailUsersRepository>(MockBehavior.Strict);
            emailUsersRepository.Setup(repository => repository.GetEmailUser(Email)).Returns(() => null);
            return emailUsersRepository;
        }

        private IOptions<EmailUserPasswordResetOptions> SetupOptions()
        {
            return Options.Create(new EmailUserPasswordResetOptions()
            {
                RunOnInitialization = RunOnInitialization,
                ExpirationTimeInMinutes = ExpirationTimeInMinutes,
                MailHomepageUrl = MailHomepageUrl,
                MailResetPasswordUrlPrefix = MailResetPasswordUrlPrefix,
                MailSupportUrl = MailSupportUrl
            });
        }

        private Mock<ISHA256TokenGenerator> SetupSHA256TokenGeneratorDefault()
        {
            Mock<ISHA256TokenGenerator> sha256TokenGenerator = new Mock<ISHA256TokenGenerator>(MockBehavior.Strict);
            sha256TokenGenerator.Setup(generator => generator.Generate()).Returns(Token);
            return sha256TokenGenerator;
        }
    }
}