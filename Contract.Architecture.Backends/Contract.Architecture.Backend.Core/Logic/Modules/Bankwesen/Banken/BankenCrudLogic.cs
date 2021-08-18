using Contract.Architecture.Backend.Core.Contract.Logic.LogicResults;
using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Logic.Tools.Identifier;
using Contract.Architecture.Backend.Core.Contract.Logic.Tools.Pagination;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Persistence.Tools.Pagination;
using Contract.Architecture.Backend.Core.Logic.LogicResults;
using Contract.Architecture.Backend.Core.Logic.Tools.Pagination;
using Microsoft.Extensions.Logging;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Modules.Bankwesen.Banken
{
    internal class BankenCrudLogic : IBankenCrudLogic
    {
        private readonly IBankenCrudRepository bankenCrudRepository;

        private readonly IGuidGenerator guidGenerator;
        private readonly ILogger<BankenCrudLogic> logger;

        public BankenCrudLogic(
            IBankenCrudRepository bankenCrudRepository,
            IGuidGenerator guidGenerator,
            ILogger<BankenCrudLogic> logger)
        {
            this.bankenCrudRepository = bankenCrudRepository;

            this.guidGenerator = guidGenerator;
            this.logger = logger;
        }

        public ILogicResult<Guid> CreateBank(IBankCreate bankCreate)
        {
            Guid newBankId = this.guidGenerator.NewGuid();
            IDbBank dbBankToCreate = Bank.CreateDbBank(newBankId, bankCreate);
            this.bankenCrudRepository.CreateBank(dbBankToCreate);

            this.logger.LogInformation($"Bank ({newBankId}) angelegt");
            return LogicResult<Guid>.Ok(newBankId);
        }

        public ILogicResult DeleteBank(Guid bankId)
        {
            if (!this.bankenCrudRepository.DoesBankExist(bankId))
            {
                this.logger.LogDebug($"Bank ({bankId}) konnte nicht gefunden werden.");
                return LogicResult.NotFound($"Bank ({bankId}) konnte nicht gefunden werden.");
            }

            // TODO: If relations are implemented, resolve conflict with the FOREIGN KEY constraint
            try
            {
                this.bankenCrudRepository.DeleteBank(bankId);
            }
            catch
            {
                this.logger.LogDebug($"Bank ({bankId}) konnte nicht gelöscht werden.");
                return LogicResult.Conflict($"Bank ({bankId}) konnte nicht gelöscht werden.");
            }

            this.logger.LogInformation($"Bank ({bankId}) gelöscht");
            return LogicResult.Ok();
        }

        public ILogicResult<IBankDetail> GetBankDetail(Guid bankId)
        {
            IDbBankDetail dbBankDetail = this.bankenCrudRepository.GetBankDetail(bankId);
            if (dbBankDetail == null)
            {
                this.logger.LogDebug($"Bank ({bankId}) konnte nicht gefunden werden.");
                return LogicResult<IBankDetail>.NotFound($"Bank ({bankId}) konnte nicht gefunden werden.");
            }

            this.logger.LogDebug($"Details für Bank ({bankId}) wurde geladen");
            return LogicResult<IBankDetail>.Ok(BankDetail.FromDbBankDetail(dbBankDetail));
        }

        public ILogicResult<IPagedResult<IBankListItem>> GetPagedBanken()
        {
            IDbPagedResult<IDbBankListItem> dbBankenPagedResult =
                this.bankenCrudRepository.GetPagedBanken();

            IPagedResult<IBankListItem> bankenPagedResult =
                PagedResult.FromDbPagedResult(
                    dbBankenPagedResult,
                    (dbBank) => BankListItem.FromDbBankListItem(dbBank));

            this.logger.LogDebug("Banken wurden geladen");
            return LogicResult<IPagedResult<IBankListItem>>.Ok(bankenPagedResult);
        }

        public ILogicResult UpdateBank(IBankUpdate bankUpdate)
        {
            IDbBank dbBankToUpdate = this.bankenCrudRepository.GetBank(bankUpdate.Id);
            if (dbBankToUpdate == null)
            {
                this.logger.LogDebug($"Bank ({bankUpdate.Id}) konnte nicht gefunden werden.");
                return LogicResult.NotFound($"Bank ({bankUpdate.Id}) konnte nicht gefunden werden.");
            }

            Bank.UpdateDbBank(dbBankToUpdate, bankUpdate);

            this.bankenCrudRepository.UpdateBank(dbBankToUpdate);

            this.logger.LogInformation($"Bank ({bankUpdate.Id}) aktualisiert");
            return LogicResult.Ok();
        }
    }
}