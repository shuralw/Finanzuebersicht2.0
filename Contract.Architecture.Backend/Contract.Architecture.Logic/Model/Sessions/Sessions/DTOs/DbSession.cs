using Contract.Architecture.Contract.Persistence.Model.Sessions.Sessions;
using System;

namespace Contract.Architecture.Logic.Model.Sessions.Sessions
{
    internal class DbSession : IDbSession
    {
        public string Token { get; set; }

        public string Name { get; set; }

        public DateTime ExpiresOn { get; set; }

        public Guid? EmailUserId { get; set; }
    }
}