using Contract.Architecture.Contract.Logic.Model.Sessions;
using Contract.Architecture.Contract.Persistence.Model.Sessions;
using System;

namespace Contract.Architecture.Logic.Model.Sessions
{
    internal class Session : ISession
    {
        public string Token { get; set; }

        public string Name { get; set; }

        public DateTime ExpiresOn { get; set; }

        public Guid? EmailUserId { get; set; }

        public static Session FromDbSession(IDbSession session)
        {
            return new Session()
            {
                Token = session.Token,
                Name = session.Name,
                ExpiresOn = session.ExpiresOn,
                EmailUserId = session.EmailUserId,
            };
        }
    }
}