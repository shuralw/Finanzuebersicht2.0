using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Bankwesen.Banken;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Kundenstamm.Kunden
{
    internal class DbKundeListItemTest : IDbKundeListItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public IDbBank Bank { get; set; }

        public static void AssertDbDefault(IDbKundeListItem dbKundeListItem)
        {
            Assert.AreEqual(KundeTestValues.IdDbDefault, dbKundeListItem.Id);
            Assert.AreEqual(KundeTestValues.NameDbDefault, dbKundeListItem.Name);
            Assert.AreEqual(KundeTestValues.BalanceDbDefault, dbKundeListItem.Balance);
            DbBankTest.AssertDbDefault(dbKundeListItem.Bank);
        }

        public static void AssertDbDefault2(IDbKundeListItem dbKundeListItem)
        {
            Assert.AreEqual(KundeTestValues.IdDbDefault2, dbKundeListItem.Id);
            Assert.AreEqual(KundeTestValues.NameDbDefault2, dbKundeListItem.Name);
            Assert.AreEqual(KundeTestValues.BalanceDbDefault2, dbKundeListItem.Balance);
            DbBankTest.AssertDbDefault2(dbKundeListItem.Bank);
        }
    }
}