using Contract.Architecture.API.Security;
using Contract.Architecture.API.Security.Authorization;
using Contract.Architecture.Contract.Logic.LogicResults;
using Contract.Architecture.Contract.Logic.Model.Sessions.Sessions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Contract.Architecture.API.Model.Sessions
{
    [ApiController]
    [Route("api/session")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionsLogic sessionsLogic;

        private readonly ILogger<SessionController> logger;

        public SessionController(
            ISessionsLogic sessionsLogic,
            ILogger<SessionController> logger)
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