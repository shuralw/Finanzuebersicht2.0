﻿using Finanzuebersicht.Backend.Core.API.Security;
using Finanzuebersicht.Backend.Core.API.Security.Authorization;
using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.SessionManagement.Sessions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Finanzuebersicht.Backend.Core.API.Modules.Sessions
{
    [ApiController]
    [Route("api/session")]
    public class SessionCrudController : ControllerBase
    {
        private readonly ISessionsCrudLogic sessionsLogic;

        private readonly ILogger<SessionCrudController> logger;

        public SessionCrudController(
            ISessionsCrudLogic sessionsLogic,
            ILogger<SessionCrudController> logger)
        {
            this.sessionsLogic = sessionsLogic;
            this.logger = logger;
        }

        [HttpGet]
        [Authorized]
        public ActionResult<ISession> GetSession()
        {
            string token = this.User.GetSessionToken();

            ILogicResult<ISession> sessionResult = this.sessionsLogic.GetSessionFromToken(token);

            return this.FromLogicResult(sessionResult);
        }

        [HttpDelete]
        [Authorized]
        [Route("logout")]
        public ActionResult Logout()
        {
            string token = this.User.GetSessionToken();
            ILogicResult terminateSessionResult = this.sessionsLogic.TerminateSession(token);

            this.logger.LogInformation("Logout successful");
            return this.FromLogicResult(terminateSessionResult);
        }
    }
}