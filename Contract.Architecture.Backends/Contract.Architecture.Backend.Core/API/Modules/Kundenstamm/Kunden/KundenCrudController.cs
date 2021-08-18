using Contract.Architecture.Backend.Core.API.Contexts.Pagination;
using Contract.Architecture.Backend.Core.API.Security.Authorization;
using Contract.Architecture.Backend.Core.Contract.Logic.LogicResults;
using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Contract.Logic.Tools.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Contract.Architecture.Backend.Core.API.Modules.Kundenstamm.Kunden
{
    [ApiController]
    [Route("api/kundenstamm/kunden")]
    public class KundenCrudController : ControllerBase
    {
        private readonly IKundenCrudLogic kundenCrudLogic;

        public KundenCrudController(IKundenCrudLogic kundenCrudLogic)
        {
            this.kundenCrudLogic = kundenCrudLogic;
        }

        [HttpGet]
        [Authorized]
        [Pagination(FilterFields = new[] { "BankId", "Name" }, SortFields = new[] { "Name" })]
        public ActionResult<IPagedResult<IKundeListItem>> GetPagedKunden()
        {
            var pagedKundenPagedResult = this.kundenCrudLogic.GetPagedKunden();
            return this.FromLogicResult(pagedKundenPagedResult);
        }

        [HttpGet]
        [Authorized]
        [Route("{kundeId}")]
        public ActionResult<IKundeDetail> GetKundeDetail(Guid kundeId)
        {
            var getKundeDetailResult = this.kundenCrudLogic.GetKundeDetail(kundeId);
            return this.FromLogicResult(getKundeDetailResult);
        }

        [HttpPost]
        [Authorized]
        public ActionResult<DataBody<Guid>> CreateKunde([FromBody] KundeCreate kundeCreate)
        {
            ILogicResult<Guid> createKundeResult = this.kundenCrudLogic.CreateKunde(kundeCreate);
            if (!createKundeResult.IsSuccessful)
            {
                return this.FromLogicResult(createKundeResult);
            }

            return this.Ok(new DataBody<Guid>(createKundeResult.Data));
        }

        [HttpPut]
        [Authorized]
        public ActionResult UpdateKunde([FromBody] KundeUpdate kundeUpdate)
        {
            ILogicResult updateKundeResult = this.kundenCrudLogic.UpdateKunde(kundeUpdate);
            return this.FromLogicResult(updateKundeResult);
        }

        [HttpDelete]
        [Authorized]
        [Route("{kundeId}")]
        public ActionResult DeleteKunde(Guid kundeId)
        {
            ILogicResult deleteKundeResult = this.kundenCrudLogic.DeleteKunde(kundeId);
            return this.FromLogicResult(deleteKundeResult);
        }
    }
}