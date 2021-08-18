using Contract.Architecture.Backend.Core.Contract.Persistence.Tools.Pagination;
using System;
using System.Collections.Generic;

namespace Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken
{
    public interface IBankenCrudRepository
    {
        void CreateBank(IDbBank bank);

        void DeleteBank(Guid bankId);

        bool DoesBankExist(Guid bankId);

        IDbBank GetBank(Guid bankId);

        IDbBankDetail GetBankDetail(Guid bankId);

        IDbPagedResult<IDbBankListItem> GetPagedBanken();

        IEnumerable<IDbBank> GetAllBanken();

        void UpdateBank(IDbBank bank);
    }
}