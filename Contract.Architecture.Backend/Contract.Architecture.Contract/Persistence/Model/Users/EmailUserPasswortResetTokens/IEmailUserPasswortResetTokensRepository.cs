using System;

namespace Contract.Architecture.Contract.Persistence.Model.Users.EmailUserPasswortReset
{
    public interface IEmailUserPasswortResetTokensRepository
    {
        void CreateToken(IDbEmailUserPasswordResetToken emailUserPasswordResetToken);

        void DeleteToken(string token);

        void DeleteToken(DateTime olderThan);

        IDbEmailUserPasswordResetToken GetToken(string token);
    }
}