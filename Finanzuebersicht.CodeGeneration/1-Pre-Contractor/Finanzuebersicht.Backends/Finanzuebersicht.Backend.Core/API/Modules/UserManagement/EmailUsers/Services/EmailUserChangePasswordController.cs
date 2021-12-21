﻿using Finanzuebersicht.Backend.Core.API.Security.Authorization;
using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.UserManagement.EmailUsers;
using Microsoft.AspNetCore.Mvc;

namespace Finanzuebersicht.Backend.Core.API.Modules.Users.EmailUsers
{
    [ApiController]
    [Route("api/users/email-user")]
    public class EmailUserChangePasswordController : ControllerBase
    {
        private readonly IEmailUserPasswordChangeLogic emailUserPasswordChangeLogic;

        public EmailUserChangePasswordController(
            IEmailUserPasswordChangeLogic emailUserPasswordChangeLogic)
        {
            this.emailUserPasswordChangeLogic = emailUserPasswordChangeLogic;
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