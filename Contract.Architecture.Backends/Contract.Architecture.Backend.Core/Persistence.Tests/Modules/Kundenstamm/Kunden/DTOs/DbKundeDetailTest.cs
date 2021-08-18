using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Bankwesen.Banken;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Kundenstamm.Kunden
{
    internal class DbKundeDetailTest : IDbKundeDetail
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public IDbBank Bank { get; set; }

        public static void AssertDbDefault(IDbKundeDetail dbKundeDetail)
        {
            Assert.AreEqual(KundeTestValues.IdDbDefault, dbKundeDetail.Id);
            Assert.AreEqual(KundeTestValues.NameDbDefault, dbKundeDetail.Name);
            Assert.AreEqual(KundeTestValues.BalanceDbDefault, dbKundeDetail.Balance);
            DbBankTest.AssertDbDefault(dbKundeDetail.Bank);
        }
    }
}