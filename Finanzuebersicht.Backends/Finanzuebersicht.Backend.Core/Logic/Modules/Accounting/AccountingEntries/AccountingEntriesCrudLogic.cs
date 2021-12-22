using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.AccountingEntries;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Identifier;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Pagination;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.Accounting.AccountingEntries;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.Accounting.Categories;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Tools.Pagination;
using Finanzuebersicht.Backend.Core.Logic.LogicResults;
using Finanzuebersicht.Backend.Core.Logic.Tools.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Finanzuebersicht.Backend.Core.Logic.Modules.Accounting.AccountingEntries
{
    internal class AccountingEntriesCrudLogic : IAccountingEntriesCrudLogic
    {
        private readonly IAccountingEntriesCrudRepository accountingEntriesCrudRepository;
        private readonly ICategoriesCrudRepository categoriesCrudRepository;

        private readonly IGuidGenerator guidGenerator;
        private readonly ILogger<AccountingEntriesCrudLogic> logger;

        public AccountingEntriesCrudLogic(
            IAccountingEntriesCrudRepository accountingEntriesCrudRepository,
            ICategoriesCrudRepository categoriesCrudRepository,
            IGuidGenerator guidGenerator,
            ILogger<AccountingEntriesCrudLogic> logger)
        {
            this.accountingEntriesCrudRepository = accountingEntriesCrudRepository;
            this.categoriesCrudRepository = categoriesCrudRepository;

            this.guidGenerator = guidGenerator;
            this.logger = logger;
        }

        public ILogicResult<Guid> CreateAccountingEntry(IAccountingEntryCreate accountingEntryCreate)
        {
            if (!this.categoriesCrudRepository.DoesCategoryExist(accountingEntryCreate.CategoryId))
            {
                this.logger.LogDebug("Category konnte nicht gefunden werden.");
                return LogicResult<Guid>.NotFound("Category konnte nicht gefunden werden.");
            }

            Guid newAccountingEntryId = this.guidGenerator.NewGuid();
            IDbAccountingEntry dbAccountingEntryToCreate = AccountingEntry.CreateDbAccountingEntry(newAccountingEntryId, accountingEntryCreate);
            this.accountingEntriesCrudRepository.CreateAccountingEntry(dbAccountingEntryToCreate);

            this.logger.LogInformation($"AccountingEntry ({newAccountingEntryId}) angelegt");
            return LogicResult<Guid>.Ok(newAccountingEntryId);
        }

        public ILogicResult DeleteAccountingEntry(Guid accountingEntryId)
        {
            if (!this.accountingEntriesCrudRepository.DoesAccountingEntryExist(accountingEntryId))
            {
                this.logger.LogDebug($"AccountingEntry ({accountingEntryId}) konnte nicht gefunden werden.");
                return LogicResult.NotFound($"AccountingEntry ({accountingEntryId}) konnte nicht gefunden werden.");
            }

            // TODO: If relations are implemented, resolve conflict with the FOREIGN KEY constraint
            try
            {
                this.accountingEntriesCrudRepository.DeleteAccountingEntry(accountingEntryId);
            }
            catch (DbUpdateException)
            {
                this.logger.LogDebug($"AccountingEntry ({accountingEntryId}) konnte nicht gelöscht werden.");
                return LogicResult.Conflict($"AccountingEntry ({accountingEntryId}) konnte nicht gelöscht werden.");
            }

            this.logger.LogInformation($"AccountingEntry ({accountingEntryId}) gelöscht");
            return LogicResult.Ok();
        }

        public ILogicResult<IAccountingEntryDetail> GetAccountingEntryDetail(Guid accountingEntryId)
        {
            IDbAccountingEntryDetail dbAccountingEntryDetail = this.accountingEntriesCrudRepository.GetAccountingEntryDetail(accountingEntryId);
            if (dbAccountingEntryDetail == null)
            {
                this.logger.LogDebug($"AccountingEntry ({accountingEntryId}) konnte nicht gefunden werden.");
                return LogicResult<IAccountingEntryDetail>.NotFound($"AccountingEntry ({accountingEntryId}) konnte nicht gefunden werden.");
            }

            this.logger.LogDebug($"Details für AccountingEntry ({accountingEntryId}) wurde geladen");
            return LogicResult<IAccountingEntryDetail>.Ok(AccountingEntryDetail.FromDbAccountingEntryDetail(dbAccountingEntryDetail));
        }

        public ILogicResult<IPagedResult<IAccountingEntryListItem>> GetPagedAccountingEntries()
        {
            IDbPagedResult<IDbAccountingEntryListItem> dbAccountingEntriesPagedResult =
                this.accountingEntriesCrudRepository.GetPagedAccountingEntries();

            IPagedResult<IAccountingEntryListItem> accountingEntriesPagedResult =
                PagedResult.FromDbPagedResult(
                    dbAccountingEntriesPagedResult,
                    (dbAccountingEntry) => AccountingEntryListItem.FromDbAccountingEntryListItem(dbAccountingEntry));

            this.logger.LogDebug("AccountingEntries wurden geladen");
            return LogicResult<IPagedResult<IAccountingEntryListItem>>.Ok(accountingEntriesPagedResult);
        }

        public ILogicResult UpdateAccountingEntry(IAccountingEntryUpdate accountingEntryUpdate)
        {
            IDbAccountingEntry dbAccountingEntryToUpdate = this.accountingEntriesCrudRepository.GetAccountingEntry(accountingEntryUpdate.Id);
            if (dbAccountingEntryToUpdate == null)
            {
                this.logger.LogDebug($"AccountingEntry ({accountingEntryUpdate.Id}) konnte nicht gefunden werden.");
                return LogicResult.NotFound($"AccountingEntry ({accountingEntryUpdate.Id}) konnte nicht gefunden werden.");
            }

            if (!this.categoriesCrudRepository.DoesCategoryExist(accountingEntryUpdate.CategoryId))
            {
                this.logger.LogDebug("Category konnte nicht gefunden werden.");
                return LogicResult.NotFound("Category konnte nicht gefunden werden.");
            }

            this.accountingEntriesCrudRepository.UpdateAccountingEntry(DbAccountingEntryUpdate
                .FromAccountingEntryUpdate(accountingEntryUpdate));

            this.logger.LogInformation($"AccountingEntry ({accountingEntryUpdate.Id}) aktualisiert");
            return LogicResult.Ok();
        }
    }
}