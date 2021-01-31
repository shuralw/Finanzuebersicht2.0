using System;

namespace Contract.Architecture.Contract.Persistence.Model.Users
{
    public interface IDbEmailUserPasswordResetToken
    {
        DateTime ExpiresOn { get; set; }

        string Token { get; set; }

        Guid EmailUserId { get; set; }
    }
}