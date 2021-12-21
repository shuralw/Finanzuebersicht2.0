using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;

namespace Finanzuebersicht.Backend.Core.Contract.Logic.Modules.LoginSystem.EmailUserLogin
{
    public interface IEmailUserLoginLogic
    {
        ILogicResult<string> LoginAsEmailUser(string email, string password);
    }
}