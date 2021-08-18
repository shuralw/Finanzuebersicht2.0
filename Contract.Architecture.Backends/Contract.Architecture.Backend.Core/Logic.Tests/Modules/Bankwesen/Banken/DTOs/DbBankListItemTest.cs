using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Contract.Persistence.Tools.Pagination;
using Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Logic.Tests.Tools.Pagination;
using System;
using System.Collections.Generic;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken
{
    internal class DbBankListItemTest : IDbBankListItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public IDbKunde Kunde { get; set; }

        public static IDbBankListItem Default()
        {
            return new DbBankListItemTest()
            {
                Id = BankTestValues.IdDefault,
                Name = BankTestValues.NameDefault,
                EroeffnetAm = BankTestValues.EroeffnetAmDefault,
                IsPleite = BankTestValues.IsPleiteDefault,
                Kunde = DbKundeTest.Default(),
            };
        }

        public static IDbBankListItem Default2()
        {
            return new DbBankListItemTest()
            {
                Id = BankTestValues.IdDefault2,
                Name = BankTestValues.NameDefault2,
                EroeffnetAm = BankTestValues.EroeffnetAmDefault2,
                IsPleite = BankTestValues.IsPleiteDefault2,
                Kunde = DbKundeTest.Default2(),
            };
        }

        public static IDbPagedResult<IDbBankListItem> ForPaged()
        {
            return new DbPagedResult<IDbBankListItem>()
            {
                Data = new List<IDbBankListItem>()
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