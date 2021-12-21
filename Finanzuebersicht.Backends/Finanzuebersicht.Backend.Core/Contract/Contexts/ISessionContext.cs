using System;

namespace Finanzuebersicht.Backend.Core.Contract
{
    public interface ISessionContext
    {
        bool IsAuthenticated { get; }

        string SessionToken { get; }

        Guid EmailUserId { get; }

        string UserName { get; }
    }
}