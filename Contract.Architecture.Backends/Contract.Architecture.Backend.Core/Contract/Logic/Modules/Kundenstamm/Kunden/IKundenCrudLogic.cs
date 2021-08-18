using Contract.Architecture.Backend.Core.Contract.Logic.LogicResults;
using Contract.Architecture.Backend.Core.Contract.Logic.Tools.Pagination;
using System;

namespace Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden
{
    public interface IKundenCrudLogic
    {
        ILogicResult<Guid> CreateKunde(IKundeCreate kundeCreate);

        ILogicResult DeleteKunde(Guid kundeId);

        ILogicResult<IKundeDetail> GetKundeDetail(Guid kundeId);

        ILogicResult<IPagedResult<IKundeListItem>> GetPagedKunden();

        ILogicResult UpdateKunde(IKundeUpdate kundeUpdate);
    }
}