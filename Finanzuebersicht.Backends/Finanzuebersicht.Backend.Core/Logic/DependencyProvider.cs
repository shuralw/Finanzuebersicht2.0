using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.AccountingEntries;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.Categories;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.CategorySearchTerms;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.LoginSystem.EmailUserLogin;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.SessionManagement.Sessions;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.UserManagement.EmailUserPasswordReset;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.UserManagement.EmailUsers;
using Finanzuebersicht.Backend.Core.Contract.Logic.SystemConnections.Email;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Identifier;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Password;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Time;
using Finanzuebersicht.Backend.Core.Logic.JobSchedulers;
using Finanzuebersicht.Backend.Core.Logic.Modules.Accounting.AccountingEntries;
using Finanzuebersicht.Backend.Core.Logic.Modules.Accounting.Categories;
using Finanzuebersicht.Backend.Core.Logic.Modules.Accounting.CategorySearchTerms;
using Finanzuebersicht.Backend.Core.Logic.Modules.LoginSystem.EmailUserLogin;
using Finanzuebersicht.Backend.Core.Logic.Modules.SessionManagement.Sessions;
using Finanzuebersicht.Backend.Core.Logic.Modules.UserManagement.EmailUserPasswordReset;
using Finanzuebersicht.Backend.Core.Logic.Modules.UserManagement.EmailUsers;
using Finanzuebersicht.Backend.Core.Logic.SystemConnections.Email;
using Finanzuebersicht.Backend.Core.Logic.Tools.Identifier;
using Finanzuebersicht.Backend.Core.Logic.Tools.Password;
using Finanzuebersicht.Backend.Core.Logic.Tools.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finanzuebersicht.Backend.Core.Logic
{
    public static class DependencyProvider
    {
        public static void Startup(IServiceCollection services, IConfiguration configuration)
        {
            StartupAccounting(services);
            StartupUsers(services, configuration);
            StartupSessions(services, configuration);
            StartupServices(services, configuration);
        }

        private static void StartupUsers(IServiceCollection services, IConfiguration configuration)
        {
            // EmailUser
            services.AddScoped<IEmailUserLoginLogic, EmailUserLoginLogic>();
            services.AddScoped<IEmailUserPasswordChangeLogic, EmailUserPasswordChangeLogic>();
            services.AddScoped<IEmailUserCrudLogic, EmailUserCrudLogic>();

            // EmailUserPasswordReset
            services.AddScoped<IEmailUserPasswordResetLogic, EmailUserPasswordResetLogic>();
            services.AddScheduledJob<EmailUserPasswordResetExpirationScheduledJob>();
            services.AddOptionsFromConfiguration<EmailUserPasswordResetOptions>(configuration);
        }

        private static void StartupSessions(IServiceCollection services, IConfiguration configuration)
        {
            // Sessions
            services.AddScoped<ISessionsCrudLogic, SessionsCrudLogic>();
            services.AddScheduledJob<SessionExpirationScheduledJob>();
            services.AddOptionsFromConfiguration<SessionExpirationOptions>(configuration);
        }

        private static void StartupServices(IServiceCollection services, IConfiguration configuration)
        {
            // Passwort
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            // DateTime
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            // E-Mail
            services.AddSingleton<IEmailClient, EmailClient>();
            services.AddOptionsFromConfiguration<EmailClientOptions>(configuration);

            // Guid
            services.AddSingleton<IGuidGenerator, GuidGenerator>();

            // SHA256 Token
            services.AddSingleton<ISHA256TokenGenerator, SHA256TokenGenerator>();
        }

        private static void StartupAccounting(IServiceCollection services)
        {
            // AccountingEntries
            services.AddScoped<IAccountingEntriesCrudLogic, AccountingEntriesCrudLogic>();

            // Categories
            services.AddScoped<ICategoriesCrudLogic, CategoriesCrudLogic>();

            // CategorySearchTerms
            services.AddScoped<ICategorySearchTermsCrudLogic, CategorySearchTermsCrudLogic>();
        }
    }
}