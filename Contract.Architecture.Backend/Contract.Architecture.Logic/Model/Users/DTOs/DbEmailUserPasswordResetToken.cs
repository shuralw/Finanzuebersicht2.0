using Contract.Architecture.Contract.Persistence.Model.Users;
using System;

namespace Contract.Architecture.Logic.Model.Users
{
    internal class DbEmailUserPasswordResetToken : IDbEmailUserPasswordResetToken
    {
        public DateTime ExpiresOn { get; set; }

        public string Token { get; set; }

        public Guid EmailUserId { get; set; }
    }
}