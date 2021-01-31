using Contract.Architecture.Contract.Logic.LogicResults;

namespace Contract.Architecture.Contract.Logic.Model.Users
{
    public interface IEmailUserPasswordResetLogic
    {
        ILogicResult InitializePasswordReset(string email, IBrowserInfo browserInfo);

        void RemoveExpiredPasswordResetTokens();

        ILogicResult ResetPassword(string token, string newPassword);
    }
}