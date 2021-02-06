using Contract.Architecture.Contract.Logic.LogicResults;
using System;

namespace Contract.Architecture.Contract.Logic.Model.Users.EmailUsers
{
    public interface IEmailUserCrudLogic
    {
        ILogicResult<Guid> CreateEmailUser(IEmailUserCreate emailUserCreate);
    }
}