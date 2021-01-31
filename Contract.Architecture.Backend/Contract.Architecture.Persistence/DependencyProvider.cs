using Contract.Architecture.Contract.Persistence.Model.Sessions;
using Contract.Architecture.Contract.Persistence.Model.Users;
using Contract.Architecture.Persistence.Model.Sessions;
using Contract.Architecture.Persistence.Model.Sessions.EfCore;
using Contract.Architecture.Persistence.Model.Users;
using Contract.Architecture.Persistence.Model.Users.EfCore;
using Microsoft.Extensions.DependencyInjection;

namespace Contract.Architecture.Persistence
{
    public static class DependencyProvider
    {
        public static void Startup(IServiceCollection services)
        {
            StartupUsers(services);
            StartupSessions(services);
        }

        private static void StartupUsers(IServiceCollection services)
        {
            services.AddDbContext<UsersDbContext>();
            services.AddScoped<IEmailUsersRepository, EmailUsersRepository>();
            services.AddScoped<IEmailUserPasswordResetTokenRepository, EmailUserPasswordResetTokenRepository>();
        }

        private static void StartupSessions(IServiceCollection services)
        {
            services.AddDbContext<SessionsDbContext>();
            services.AddScoped<ISessionsRepository, SessionsRepository>();
        }
    }
}