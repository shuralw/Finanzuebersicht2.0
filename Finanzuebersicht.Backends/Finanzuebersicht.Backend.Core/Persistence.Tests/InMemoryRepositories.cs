using Finanzuebersicht.Backend.Core.Persistence.Modules.SessionManagement.Sessions;
using Finanzuebersicht.Backend.Core.Persistence.Modules.UserManagement.EmailUserPasswortReset;
using Finanzuebersicht.Backend.Core.Persistence.Modules.UserManagement.EmailUsers;

namespace Finanzuebersicht.Backend.Core.Persistence.Tests
{
    public class InMemoryRepositories
    {
        private PersistenceDbContext persistenceDbContext;

        public InMemoryRepositories()
        {
            this.persistenceDbContext = InMemoryDbContext.CreatePersistenceDbContextEmpty();
        }

        internal EmailUsersRepository GetEmailUsersRepository()
        {
            return new EmailUsersRepository(this.persistenceDbContext);
        }

        internal EmailUserPasswortResetTokensRepository GetEmailUserPasswordResetTokenRepository()
        {
            return new EmailUserPasswortResetTokensRepository(this.persistenceDbContext);
        }

        internal SessionsRepository GetSessionsRepository()
        {
            return new SessionsRepository(this.persistenceDbContext);
        }
    }
}