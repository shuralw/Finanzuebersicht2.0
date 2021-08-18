using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden
{
    internal class KundeDetailTest : IKundeDetail
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public IBank Bank { get; set; }

        public static IKundeDetail Default()
        {
            return new KundeDetailTest()
            {
                Id = KundeTestValues.IdDefault,
                Name = KundeTestValues.NameDefault,
                Balance = KundeTestValues.BalanceDefault,
            };
        }

        public static IKundeDetail Default2()
        {
            return new KundeDetailTest()
            {
                Id = KundeTestValues.IdDefault2,
                Name = KundeTestValues.NameDefault2,
                Balance = KundeTestValues.BalanceDefault2,
            };
        }

        public static void AssertDefault(IKundeDetail kundeDetail)
        {
            Assert.AreEqual(KundeTestValues.IdDefault, kundeDetail.Id);
            Assert.AreEqual(KundeTestValues.NameDefault, kundeDetail.Name);
            Assert.AreEqual(KundeTestValues.BalanceDefault, kundeDetail.Balance);
            BankTest.AssertDefault(kundeDetail.Bank);
        }

        public static void AssertDefault2(IKundeDetail kundeDetail)
        {
            Assert.AreEqual(KundeTestValues.IdDefault2, kundeDetail.Id);
            Assert.AreEqual(KundeTestValues.NameDefault2, kundeDetail.Name);
            Assert.AreEqual(KundeTestValues.BalanceDefault2, kundeDetail.Balance);
            BankTest.AssertDefault2(kundeDetail.Bank);
        }
    }
}