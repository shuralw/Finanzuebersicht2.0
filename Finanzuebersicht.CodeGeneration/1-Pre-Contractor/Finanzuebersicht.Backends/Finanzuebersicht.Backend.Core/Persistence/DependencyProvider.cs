using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.SessionManagement.Sessions;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.UserManagement.EmailUserPasswortResetTokens;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.UserManagement.EmailUsers;
using Finanzuebersicht.Backend.Core.Persistence.Modules.SessionManagement.Sessions;
using Finanzuebersicht.Backend.Core.Persistence.Modules.UserManagement.EmailUserPasswortReset;
using Finanzuebersicht.Backend.Core.Persistence.Modules.UserManagement.EmailUsers;
using Microsoft.Extensions.DependencyInjection;

namespace Finanzuebersicht.Backend.Core.Persistence
{
    public static class DependencyProvider
    {
        public static void Startup(IServiceCollection services)
        {
            services.AddDbContext<PersistenceDbContext>();

            StartupUsers(services);
            StartupSessions(services);
        }

        private static void StartupUsers(IServiceCollection services)
        {
            services.AddScoped<IEmailUsersRepository, EmailUsersRepository>();
            services.AddScoped<IEmailUserPasswortResetTokensRepository, EmailUserPasswortResetTokensRepository>();
        }

        private static void StartupSessions(IServiceCollection services)
        {
            services.AddScoped<ISessionsRepository, SessionsRepository>();
        }
    }
}