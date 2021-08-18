using Contract.Architecture.Backend.Core.Contract.Logic.LogicResults;
using Contract.Architecture.Backend.Core.Contract.Logic.Tools.Pagination;
using System;

namespace Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken
{
    public interface IBankenCrudLogic
    {
        ILogicResult<Guid> CreateBank(IBankCreate bankCreate);

        ILogicResult DeleteBank(Guid bankId);

        ILogicResult<IBankDetail> GetBankDetail(Guid bankId);

        ILogicResult<IPagedResult<IBankListItem>> GetPagedBanken();

        ILogicResult UpdateBank(IBankUpdate bankUpdate);
    }
}