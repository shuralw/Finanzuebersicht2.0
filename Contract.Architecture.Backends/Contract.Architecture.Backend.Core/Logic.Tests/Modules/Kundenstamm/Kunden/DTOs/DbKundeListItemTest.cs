using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Contract.Persistence.Tools.Pagination;
using Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Logic.Tests.Tools.Pagination;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden
{
    internal class DbKundeListItemTest : IDbKundeListItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public IDbBank Bank { get; set; }

        public static IDbKundeListItem Default()
        {
            return new DbKundeListItemTest()
            {
                Id = KundeTestValues.IdDefault,
                Name = KundeTestValues.NameDefault,
                Balance = KundeTestValues.BalanceDefault,
                Bank = DbBankTest.Default(),
            };
        }

        public static IDbKundeListItem Default2()
        {
            return new DbKundeListItemTest()
            {
                Id = KundeTestValues.IdDefault2,
                Name = KundeTestValues.NameDefault2,
                Balance = KundeTestValues.BalanceDefault2,
                Bank = DbBankTest.Default2(),
            };
        }

        public static IDbPagedResult<IDbKundeListItem> ForPaged()
        {
            return new DbPagedResult<IDbKundeListItem>()
            {
                Data = new List<IDbKundeListItem>()
                {
                    Default(),
                    Default2()
                },
                TotalCount = 2,
                Count = 2,
                Limit = 10,
                Offset = 0
            };
        }
    }
}