using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken
{
    internal class BankListItemTest : IBankListItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public IKunde Kunde { get; set; }

        public static void AssertDefault(IBankListItem bankListItem)
        {
            Assert.AreEqual(BankTestValues.IdDefault, bankListItem.Id);
            Assert.AreEqual(BankTestValues.NameDefault, bankListItem.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmDefault, bankListItem.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteDefault, bankListItem.IsPleite);
            KundeTest.AssertDefault(bankListItem.Kunde);
        }

        public static void AssertDefault2(IBankListItem bankListItem)
        {
            Assert.AreEqual(BankTestValues.IdDefault2, bankListItem.Id);
            Assert.AreEqual(BankTestValues.NameDefault2, bankListItem.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmDefault2, bankListItem.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteDefault2, bankListItem.IsPleite);
            KundeTest.AssertDefault(bankListItem.Kunde);
        }
    }
}