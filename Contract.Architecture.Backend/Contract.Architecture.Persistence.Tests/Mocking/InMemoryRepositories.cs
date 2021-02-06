using Contract.Architecture.Persistence.Model;
using Contract.Architecture.Persistence.Model.Sessions.Sessions;
using Contract.Architecture.Persistence.Model.Users.EmailUserPasswortReset;
using Contract.Architecture.Persistence.Model.Users.EmailUsers;
using Microsoft.EntityFrameworkCore;

namespace Contract.Architecture.Persistence.Tests.Mocking
{
    public class InMemoryRepositories
    {
        private PersistenceDbContext persistenceDbContext;

        public InMemoryRepositories()
        {
            this.CreatePersistenceDbContext();
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

        private void CreatePersistenceDbContext()
        {
            DbContextOptions<PersistenceDbContext> options;
            var builder = new DbContextOptionsBuilder<PersistenceDbContext>();
            builder.UseInMemoryDatabase("in-memory-db");
            options = builder.Options;

            this.persistenceDbContext = PersistenceDbContext.CustomInstantiate(options);
            this.persistenceDbContext.Database.EnsureDeleted();
            this.persistenceDbContext.Database.EnsureCreated();
        }
    }
}