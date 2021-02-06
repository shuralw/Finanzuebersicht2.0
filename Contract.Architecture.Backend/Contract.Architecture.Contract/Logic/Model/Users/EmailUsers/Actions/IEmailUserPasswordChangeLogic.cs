using Contract.Architecture.Contract.Logic.LogicResults;

namespace Contract.Architecture.Contract.Logic.Model.Users.EmailUsers
{
    public interface IEmailUserPasswordChangeLogic
    {
        ILogicResult ChangePassword(string oldPassword, string newPassword);
    }
}