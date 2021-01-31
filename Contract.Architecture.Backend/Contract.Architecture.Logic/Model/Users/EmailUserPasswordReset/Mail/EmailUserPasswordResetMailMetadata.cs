using Contract.Architecture.Contract.Logic.Model.Users;

namespace Contract.Architecture.Logic.Model.Users
{
    internal class EmailUserPasswordResetMailMetadata
    {
        public string MailResetPasswordUrlPrefix { get; set; }

        public string MailHomepageUrl { get; set; }

        public string MailSupportUrl { get; set; }

        public string EmailUserEmail { get; set; }

        public string EmailUserPasswordResetToken { get; set; }

        public IBrowserInfo EmailUserBrowserInfo { get; set; }
    }
}