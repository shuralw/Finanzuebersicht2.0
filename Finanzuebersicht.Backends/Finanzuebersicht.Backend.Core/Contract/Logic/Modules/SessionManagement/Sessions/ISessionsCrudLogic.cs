using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;
using System;

namespace Finanzuebersicht.Backend.Core.Contract.Logic.Modules.SessionManagement.Sessions
{
    public interface ISessionsCrudLogic
    {
        string CreateSessionForEmailUser(Guid emailUserId, string name);

        ILogicResult<ISession> GetSessionFromToken(string token);

        ILogicResult TerminateSession(string token);
    }
}