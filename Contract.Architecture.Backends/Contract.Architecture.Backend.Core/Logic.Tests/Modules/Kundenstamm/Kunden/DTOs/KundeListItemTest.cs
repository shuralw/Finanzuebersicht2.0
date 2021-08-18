using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden
{
    internal class KundeListItemTest : IKundeListItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public IBank Bank { get; set; }

        public static void AssertDefault(IKundeListItem kundeListItem)
        {
            Assert.AreEqual(KundeTestValues.IdDefault, kundeListItem.Id);
            Assert.AreEqual(KundeTestValues.NameDefault, kundeListItem.Name);
            Assert.AreEqual(KundeTestValues.BalanceDefault, kundeListItem.Balance);
            BankTest.AssertDefault(kundeListItem.Bank);
        }

        public static void AssertDefault2(IKundeListItem kundeListItem)
        {
            Assert.AreEqual(KundeTestValues.IdDefault2, kundeListItem.Id);
            Assert.AreEqual(KundeTestValues.NameDefault2, kundeListItem.Name);
            Assert.AreEqual(KundeTestValues.BalanceDefault2, kundeListItem.Balance);
            BankTest.AssertDefault2(kundeListItem.Bank);
        }
    }
}