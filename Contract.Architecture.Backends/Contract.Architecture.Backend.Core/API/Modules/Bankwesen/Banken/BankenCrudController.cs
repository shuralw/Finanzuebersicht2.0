using Contract.Architecture.Backend.Core.API.Contexts.Pagination;
using Contract.Architecture.Backend.Core.API.Security.Authorization;
using Contract.Architecture.Backend.Core.Contract.Logic.LogicResults;
using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Logic.Tools.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Contract.Architecture.Backend.Core.API.Modules.Bankwesen.Banken
{
    [ApiController]
    [Route("api/bankwesen/banken")]
    public class BankenCrudController : ControllerBase
    {
        private readonly IBankenCrudLogic bankenCrudLogic;

        public BankenCrudController(IBankenCrudLogic bankenCrudLogic)
        {
            this.bankenCrudLogic = bankenCrudLogic;
        }

        [HttpGet]
        [Authorized]
        [Pagination(FilterFields = new[] { "Name" }, SortFields = new[] { "Name" })]
        public ActionResult<IPagedResult<IBankListItem>> GetPagedBanken()
        {
            var pagedBankenPagedResult = this.bankenCrudLogic.GetPagedBanken();
            return this.FromLogicResult(pagedBankenPagedResult);
        }

        [HttpGet]
        [Authorized]
        [Route("{bankId}")]
        public ActionResult<IBankDetail> GetBankDetail(Guid bankId)
        {
            var getBankDetailResult = this.bankenCrudLogic.GetBankDetail(bankId);
            return this.FromLogicResult(getBankDetailResult);
        }

        [HttpPost]
        [Authorized]
        public ActionResult<DataBody<Guid>> CreateBank([FromBody] BankCreate bankCreate)
        {
            ILogicResult<Guid> createBankResult = this.bankenCrudLogic.CreateBank(bankCreate);
            if (!createBankResult.IsSuccessful)
            {
                return this.FromLogicResult(createBankResult);
            }

            return this.Ok(new DataBody<Guid>(createBankResult.Data));
        }

        [HttpPut]
        [Authorized]
        public ActionResult UpdateBank([FromBody] BankUpdate bankUpdate)
        {
            ILogicResult updateBankResult = this.bankenCrudLogic.UpdateBank(bankUpdate);
            return this.FromLogicResult(updateBankResult);
        }

        [HttpDelete]
        [Authorized]
        [Route("{bankId}")]
        public ActionResult DeleteBank(Guid bankId)
        {
            ILogicResult deleteBankResult = this.bankenCrudLogic.DeleteBank(bankId);
            return this.FromLogicResult(deleteBankResult);
        }
    }
}