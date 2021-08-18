using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Persistence.Tools.Pagination;
using Contract.Architecture.Backend.Core.Logic.Tests.Tools.Pagination;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        public static IDbBankListItem Default()
        {
            return new DbBankListItemTest()
            {
                Id = BankTestValues.IdDefault,
                Name = BankTestValues.NameDefault,
                EroeffnetAm = BankTestValues.EroeffnetAmDefault,
                IsPleite = BankTestValues.IsPleiteDefault,
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