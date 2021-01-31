using Contract.Architecture.Contract.Logic.LogicResults;
using System;

namespace Contract.Architecture.Contract.Logic.Model.Users
{
    public interface IEmailUserRegistrationLogic
    {
        ILogicResult<Guid> Register(string email, string password);
    }
}