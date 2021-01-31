using System;

namespace Contract.Architecture.Contract
{
    public interface ISessionContext
    {
        bool IsAuthenticated { get; }

        string SessionToken { get; }

        Guid EmailUserId { get; }

        string UserName { get; }
    }
}