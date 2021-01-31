using Contract.Architecture.Contract.Logic.Model.Users;
using Contract.Architecture.Contract.Logic.Services.ScheduledJob;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Contract.Architecture.Logic.Model.Users
{
    internal class EmailUserPasswordResetExpirationScheduledJob : IScheduledJob
    {
        private readonly IEmailUserPasswordResetLogic emailUserPasswordResetLogic;

        private readonly EmailUserPasswordResetOptions options;

        public EmailUserPasswordResetExpirationScheduledJob(
            IEmailUserPasswordResetLogic emailUserPasswordResetLogic,
            IOptions<EmailUserPasswordResetOptions> options)
        {
            this.emailUserPasswordResetLogic = emailUserPasswordResetLogic;

            this.options = options.Value;
        }

        public int GetDelayInSeconds() => this.options.ExpirationTimeInMinutes * 60;

        public bool IsExecutingOnInitialization() => this.options.RunOnInitialization;

        public Task Execute()
        {
            this.emailUserPasswordResetLogic.RemoveExpiredPasswordResetTokens();

            return Task.CompletedTask;
        }
    }
}