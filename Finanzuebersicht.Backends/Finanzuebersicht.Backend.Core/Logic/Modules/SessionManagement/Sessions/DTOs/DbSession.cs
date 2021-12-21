using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.SessionManagement.Sessions;
using System;

namespace Finanzuebersicht.Backend.Core.Logic.Modules.SessionManagement.Sessions
{
    internal class DbSession : IDbSession
    {
        public string Token { get; set; }

        public string Name { get; set; }

        public DateTime ExpiresOn { get; set; }

        public Guid? EmailUserId { get; set; }
    }
}