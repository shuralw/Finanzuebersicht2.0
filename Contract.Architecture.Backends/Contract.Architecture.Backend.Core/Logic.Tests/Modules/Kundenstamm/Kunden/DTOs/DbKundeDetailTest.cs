using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden
{
    internal class DbKundeDetailTest : IDbKundeDetail
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public IDbBank Bank { get; set; }

        public static IDbKundeDetail Default()
        {
            return new DbKundeDetailTest()
            {
                Id = KundeTestValues.IdDefault,
                Name = KundeTestValues.NameDefault,
                Balance = KundeTestValues.BalanceDefault,
                Bank = DbBankTest.Default(),
            };
        }

        public static IDbKundeDetail Default2()
        {
            return new DbKundeDetailTest()
            {
                Id = KundeTestValues.IdDefault,
                Name = KundeTestValues.NameDefault2,
                Balance = KundeTestValues.BalanceDefault2,
                Bank = DbBankTest.Default2(),
            };
        }

        public static void AssertDefault(IDbKundeDetail dbKundeDetail)
        {
            Assert.AreEqual(KundeTestValues.IdDefault, dbKundeDetail.Id);
            DbBankTest.AssertDefault(dbKundeDetail.Bank);
        }

        public static void AssertDefault2(IDbKundeDetail dbKundeDetail)
        {
            Assert.AreEqual(KundeTestValues.IdDefault2, dbKundeDetail.Id);
            Assert.AreEqual(KundeTestValues.NameDefault2, dbKundeDetail.Name);
            Assert.AreEqual(KundeTestValues.BalanceDefault2, dbKundeDetail.Balance);
            DbBankTest.AssertDefault2(dbKundeDetail.Bank);
        }
    }
}