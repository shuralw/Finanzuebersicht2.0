using Contract.Architecture.Contract.Persistence.Model.Sessions;
using System;

namespace Contract.Architecture.Logic.Model.Sessions
{
    internal class DbSession : IDbSession
    {
        public string Token { get; set; }

        public string Name { get; set; }

        public DateTime ExpiresOn { get; set; }

        public Guid? EmailUserId { get; set; }
    }
}