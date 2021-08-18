using Contract.Architecture.Backend.Core.Contract.Persistence.Tools.Pagination;
using System;
using System.Collections.Generic;

namespace Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden
{
    public interface IKundenCrudRepository
    {
        void CreateKunde(IDbKunde kunde);

        void DeleteKunde(Guid kundeId);

        bool DoesKundeExist(Guid kundeId);

        IDbKunde GetKunde(Guid kundeId);

        IDbKundeDetail GetKundeDetail(Guid kundeId);

        IDbPagedResult<IDbKundeListItem> GetPagedKunden();

        IEnumerable<IDbKunde> GetAllKunden();

        void UpdateKunde(IDbKunde kunde);
    }
}