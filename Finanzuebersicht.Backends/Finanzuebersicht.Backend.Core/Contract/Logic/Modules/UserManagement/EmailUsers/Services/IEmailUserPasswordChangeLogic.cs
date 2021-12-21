using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;

namespace Finanzuebersicht.Backend.Core.Contract.Logic.Modules.UserManagement.EmailUsers
{
    public interface IEmailUserPasswordChangeLogic
    {
        ILogicResult ChangePassword(string oldPassword, string newPassword);
    }
}