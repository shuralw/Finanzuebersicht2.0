using Contract.Architecture.Contract.Logic.LogicResults;
using Contract.Architecture.Contract.Logic.Model.Users;
using Contract.Architecture.Contract.Logic.Services.Identifier;
using Contract.Architecture.Contract.Logic.Services.Password;
using Contract.Architecture.Contract.Persistence.Model.Users;
using Contract.Architecture.Logic.LogicResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Contract.Architecture.Logic.Model.Users
{
    internal class EmailUserRegistrationLogic : IEmailUserRegistrationLogic
    {
        private readonly IEmailUsersRepository emailUsersRepository;

        private readonly IBsiPasswordService bsiPasswordService;
        private readonly IGuidGenerator guidGenerator;
        private readonly ILogger<EmailUserRegistrationLogic> logger;

        public EmailUserRegistrationLogic(
            IEmailUsersRepository emailUsersRepository,
            IBsiPasswordService bsiPasswordService,
            IGuidGenerator guidGenerator,
            ILogger<EmailUserRegistrationLogic> logger)
        {
            this.emailUsersRepository = emailUsersRepository;

            this.bsiPasswordService = bsiPasswordService;
            this.guidGenerator = guidGenerator;
            this.logger = logger;
        }

        public ILogicResult<Guid> Register(string email, string password)
        {
            email = email.ToLower();
            if (this.emailUsersRepository.GetEmailUser(email) != null)
            {
                this.logger.LogDebug("EmailUser mit dieser E-Mail-Addresse existiert bereits.");
                return LogicResult<Guid>.Conflict("EmailUser mit dieser E-Mail-Addresse existiert bereits.");
            }

            DbEmailUser emailUserToAdd = this.CreateNewEmailUser(email, password);

            this.emailUsersRepository.CreateEmailUser(emailUserToAdd);

            this.logger.LogInformation("EmailUser ({email}) angelegt", email);
            return LogicResult<Guid>.Ok(emailUserToAdd.Id);
        }

        private DbEmailUser CreateNewEmailUser(string email, string password)
        {
            IBSIPasswordHash passwordHash = this.bsiPasswordService.HashPassword(password);

            return new DbEmailUser()
            {
                Id = this.guidGenerator.NewGuid(),
                Email = email,
                PasswordHash = passwordHash.PasswordHash,
                PasswordSalt = passwordHash.Salt
            };
        }
    }
}