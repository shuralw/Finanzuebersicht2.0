using Contract.Architecture.Contract.Logic.Model.Sessions;
using Contract.Architecture.Contract.Logic.Model.Users;
using Contract.Architecture.Contract.Logic.Services.Email;
using Contract.Architecture.Contract.Logic.Services.Identifier;
using Contract.Architecture.Contract.Logic.Services.Password;
using Contract.Architecture.Contract.Logic.Services.Time;
using Contract.Architecture.Logic.Model.Sessions;
using Contract.Architecture.Logic.Model.Users;
using Contract.Architecture.Logic.Services.Email;
using Contract.Architecture.Logic.Services.Identifier;
using Contract.Architecture.Logic.Services.Password;
using Contract.Architecture.Logic.Services.ScheduledJobs;
using Contract.Architecture.Logic.Services.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contract.Architecture.Logic
{
    public static class DependencyProvider
    {
        public static void Startup(IServiceCollection services, IConfiguration configuration)
        {
            StartupUsers(services, configuration);
            StartupSessions(services, configuration);
            StartupServices(services, configuration);
        }

        private static void StartupUsers(IServiceCollection services, IConfiguration configuration)
        {
            // EmailUser
            services.AddScoped<IEmailUserLoginLogic, EmailUserLoginLogic>();
            services.AddScoped<IEmailUserPasswordChangeLogic, EmailUserPasswordChangeLogic>();
            services.AddScoped<IEmailUserPasswordResetLogic, EmailUserPasswordResetLogic>();
            services.AddScoped<IEmailUserRegistrationLogic, EmailUserRegistrationLogic>();
            services.AddScheduledJob<EmailUserPasswordResetExpirationScheduledJob>();
            services.AddOptionsFromConfiguration<EmailUserPasswordResetOptions>(configuration);
        }

        private static void StartupSessions(IServiceCollection services, IConfiguration configuration)
        {
            // Sessions
            services.AddScoped<ISessionsLogic, SessionsLogic>();
            services.AddScheduledJob<SessionExpirationScheduledJob>();
            services.AddOptionsFromConfiguration<SessionExpirationOptions>(configuration);
        }

        private static void StartupServices(IServiceCollection services, IConfiguration configuration)
        {
            // BSI Passwort
            services.AddSingleton<IBsiPasswordService, BsiPasswordService>();

            // DateTime
            services.AddSingleton<IDateTimeService, DateTimeService>();

            // E-Mail
            services.AddSingleton<IEmailService, EmailService>();
            services.AddOptionsFromConfiguration<EmailServiceOptions>(configuration);

            // Guid
            services.AddSingleton<IGuidGenerator, GuidGenerator>();

            // SHA256 Token
            services.AddSingleton<ISHA256TokenGenerator, SHA256TokenGenerator>();
        }
    }
}