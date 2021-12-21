using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;
using System;

namespace Finanzuebersicht.Backend.Core.Contract.Logic.Modules.UserManagement.EmailUsers
{
    public interface IEmailUserCrudLogic
    {
        ILogicResult<Guid> CreateEmailUser(IEmailUserCreate emailUserCreate);
    }
}