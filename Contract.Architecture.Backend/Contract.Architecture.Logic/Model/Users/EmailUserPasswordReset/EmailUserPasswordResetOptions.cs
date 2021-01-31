﻿namespace Contract.Architecture.Logic.Model.Users
{
    internal class EmailUserPasswordResetOptions : OptionsFromConfiguration
    {
        public override string Position => "ContractArchitecture:Users:EmailUser:PasswordReset";

        public bool RunOnInitialization { get; set; }

        public int ExpirationTimeInMinutes { get; set; }

        public string MailResetPasswordUrlPrefix { get; set; }

        public string MailHomepageUrl { get; set; }

        public string MailSupportUrl { get; set; }
    }
}