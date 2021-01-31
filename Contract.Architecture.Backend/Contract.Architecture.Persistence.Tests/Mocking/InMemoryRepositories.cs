using Contract.Architecture.Persistence.Model.Sessions;
using Contract.Architecture.Persistence.Model.Sessions.EfCore;
using Contract.Architecture.Persistence.Model.Users;
using Contract.Architecture.Persistence.Model.Users.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Contract.Architecture.Persistence.Tests.Mocking
{
    public class InMemoryRepositories
    {
        private UsersDbContext usersDbContext;
        private SessionsDbContext sessionsDbContext;

        public InMemoryRepositories()
        {
            this.CreateUsersDbContext();
            this.CreateSessionsDbContext();
        }

        internal EmailUsersRepository GetEmailUsersRepository()
        {
            return new EmailUsersRepository(this.usersDbContext);
        }

        internal EmailUserPasswordResetTokenRepository GetEmailUserPasswordResetTokenRepository()
        {
            return new EmailUserPasswordResetTokenRepository(this.usersDbContext);
        }

        internal SessionsRepository GetSessionsRepository()
        {
            return new SessionsRepository(this.sessionsDbContext);
        }

        private void CreateUsersDbContext()
        {
            DbContextOptions<UsersDbContext> options;
            var builder = new DbContextOptionsBuilder<UsersDbContext>();
            builder.UseInMemoryDatabase("in-memory-db");
            options = builder.Options;

            this.usersDbContext = UsersDbContext.CustomInstantiate(options);
            this.usersDbContext.Database.EnsureDeleted();
            this.usersDbContext.Database.EnsureCreated();
        }

        private void CreateSessionsDbContext()
        {
            DbContextOptions<SessionsDbContext> options;
            var builder = new DbContextOptionsBuilder<SessionsDbContext>();
            builder.UseInMemoryDatabase("in-memory-db");
            options = builder.Options;

            this.sessionsDbContext = SessionsDbContext.CustomInstantiate(options);
            this.sessionsDbContext.Database.EnsureDeleted();
            this.sessionsDbContext.Database.EnsureCreated();
        }
    }
}