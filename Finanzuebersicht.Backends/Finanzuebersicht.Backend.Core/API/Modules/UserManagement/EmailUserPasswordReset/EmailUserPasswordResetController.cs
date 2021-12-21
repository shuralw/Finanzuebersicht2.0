﻿using Finanzuebersicht.Backend.Core.API.Tools.UserAgent;
using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.UserManagement.EmailUserPasswordReset;
using Microsoft.AspNetCore.Mvc;

namespace Finanzuebersicht.Backend.Core.API.Modules.Users.EmailUserPasswordReset
{
    [ApiController]
    [Route("api/users/email-user")]
    public class EmailUserPasswordResetController : ControllerBase
    {
        private readonly IEmailUserPasswordResetLogic emailUserPasswordResetLogic;

        public EmailUserPasswordResetController(
            IEmailUserPasswordResetLogic emailUserPasswordResetLogic)
        {
            this.emailUserPasswordResetLogic = emailUserPasswordResetLogic;
        }

        [HttpPost]
        [Route("forgot-password")]
        public ActionResult ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            var browser = UserAgentParser.GetBrowser(this.Request);
            var operatingSystem = UserAgentParser.GetOperatingSystem(this.Request);

            ILogicResult result = this.emailUserPasswordResetLogic.InitializePasswordReset(forgotPassword.Email, new BrowserInfo()
            {
                Browser = browser,
                OperatingSystem = operatingSystem
            });

            return this.FromLogicResult(result);
        }

        [HttpPut]
        [Route("reset-password")]
        public ActionResult ResetPassword([FromBody] ResetPassword resetPassword)
        {
            ILogicResult result = this.emailUserPasswordResetLogic.ResetPassword(resetPassword.Token, resetPassword.NewPassword);
            return this.FromLogicResult(result);
        }
    }
}