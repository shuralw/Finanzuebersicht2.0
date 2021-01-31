using Contract.Architecture.API.Security.Authorization;
using Contract.Architecture.API.Services.RequestEmailUserAgentInfo;
using Contract.Architecture.Contract.Logic.LogicResults;
using Contract.Architecture.Contract.Logic.Model.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Contract.Architecture.API.Model.Users
{
    [ApiController]
    [Route("api/users/email-user")]
    public class EmailUserController : ControllerBase
    {
        private readonly IEmailUserLoginLogic emailUserLoginLogic;
        private readonly IEmailUserPasswordChangeLogic emailUserPasswordChangeLogic;
        private readonly IEmailUserPasswordResetLogic emailUserPasswordResetLogic;
        private readonly IEmailUserRegistrationLogic emailUsersLogic;

        public EmailUserController(
            IEmailUserRegistrationLogic emailUsersLogic,
            IEmailUserPasswordResetLogic emailUserPasswordResetLogic,
            IEmailUserPasswordChangeLogic emailUserPasswordChangeLogic,
            IEmailUserLoginLogic emailUserLoginLogic)
        {
            this.emailUsersLogic = emailUsersLogic;
            this.emailUserLoginLogic = emailUserLoginLogic;
            this.emailUserPasswordChangeLogic = emailUserPasswordChangeLogic;
            this.emailUserPasswordResetLogic = emailUserPasswordResetLogic;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<DataBody<Guid>> CreateEmailUser([FromBody] EmailUserRegister emailUserRegister)
        {
            ILogicResult<Guid> createEmailUserResult = this.emailUsersLogic.Register(emailUserRegister.Email, emailUserRegister.Password);
            if (!createEmailUserResult.IsSuccessful)
            {
                return this.FromLogicResult(createEmailUserResult);
            }

            return this.Ok(new DataBody<Guid>(createEmailUserResult.Data));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public ActionResult<DataBody<string>> LoginAsEmailUser([FromBody] EmailUserLogin emailUserLogin)
        {
            var loginAsEmailUserResult = this.emailUserLoginLogic.LoginAsEmailUser(emailUserLogin.Email, emailUserLogin.Password);

            if (!loginAsEmailUserResult.IsSuccessful)
            {
                return this.FromLogicResult(loginAsEmailUserResult);
            }

            return this.Ok(new DataBody<string>(loginAsEmailUserResult.Data));
        }

        [HttpPost]
        [Route("forgot-password")]
        public ActionResult ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            var browser = RequestEmailUserAgentInfo.GetBrowser(this.Request);
            var operatingSystem = RequestEmailUserAgentInfo.GetOperatingSystem(this.Request);

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

        [HttpPut]
        [Authorized]
        [Route("change-password")]
        public ActionResult ChangePassword([FromBody] ChangePassword changePassword)
        {
            ILogicResult result = this.emailUserPasswordChangeLogic.ChangePassword(
                changePassword.OldPassword,
                changePassword.NewPassword);
            return this.FromLogicResult(result);
        }
    }
}