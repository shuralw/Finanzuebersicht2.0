using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Pagination;
using System;

namespace Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.AccountingEntries
{
    public interface IAccountingEntriesCrudLogic
    {
        ILogicResult<Guid> CreateAccountingEntry(IAccountingEntryCreate accountingEntryCreate);

        ILogicResult DeleteAccountingEntry(Guid accountingEntryId);

        ILogicResult<IAccountingEntryDetail> GetAccountingEntryDetail(Guid accountingEntryId);

        ILogicResult<IPagedResult<IAccountingEntryListItem>> GetPagedAccountingEntries();

        ILogicResult UpdateAccountingEntry(IAccountingEntryUpdate accountingEntryUpdate);
    }
}