using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.UserManagement.EmailUsers;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Identifier;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Password;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.UserManagement.EmailUsers;
using Finanzuebersicht.Backend.Core.Logic.LogicResults;
using Microsoft.Extensions.Logging;
using System;

namespace Finanzuebersicht.Backend.Core.Logic.Modules.UserManagement.EmailUsers
{
    internal class EmailUserCrudLogic : IEmailUserCrudLogic
    {
        private readonly IEmailUsersRepository emailUsersRepository;

        private readonly IPasswordHasher passwordHasher;
        private readonly IGuidGenerator guidGenerator;
        private readonly ILogger<EmailUserCrudLogic> logger;

        public EmailUserCrudLogic(
            IEmailUsersRepository emailUsersRepository,
            IPasswordHasher passwordHasher,
            IGuidGenerator guidGenerator,
            ILogger<EmailUserCrudLogic> logger)
        {
            this.emailUsersRepository = emailUsersRepository;

            this.passwordHasher = passwordHasher;
            this.guidGenerator = guidGenerator;
            this.logger = logger;
        }

        public ILogicResult<Guid> CreateEmailUser(IEmailUserCreate emailUserCreate)
        {
            emailUserCreate.Email = emailUserCreate.Email.ToLower();
            if (this.emailUsersRepository.GetEmailUser(emailUserCreate.Email) != null)
            {
                this.logger.LogDebug("EmailUser mit dieser E-Mail-Addresse existiert bereits.");
                return LogicResult<Guid>.Conflict("EmailUser mit dieser E-Mail-Addresse existiert bereits.");
            }

            DbEmailUser emailUserToAdd = this.CreateNewEmailUser(emailUserCreate);

            this.emailUsersRepository.CreateEmailUser(emailUserToAdd);

            this.logger.LogInformation("EmailUser ({email}) angelegt", emailUserCreate.Email);
            return LogicResult<Guid>.Ok(emailUserToAdd.Id);
        }

        private DbEmailUser CreateNewEmailUser(IEmailUserCreate emailUserCreate)
        {
            IPasswordHash passwordHash = this.passwordHasher.HashPassword(emailUserCreate.Password);

            return new DbEmailUser()
            {
                Id = this.guidGenerator.NewGuid(),
                Email = emailUserCreate.Email,
                PasswordHash = passwordHash.Hash,
                PasswordSalt = passwordHash.Salt
            };
        }
    }
}