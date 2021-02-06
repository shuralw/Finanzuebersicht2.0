using Contract.Architecture.Contract.Logic.LogicResults;

namespace Contract.Architecture.Contract.Logic.Model.Users.EmailUsers
{
    public interface IEmailUserLoginLogic
    {
        ILogicResult<string> LoginAsEmailUser(string email, string password);
    }
}