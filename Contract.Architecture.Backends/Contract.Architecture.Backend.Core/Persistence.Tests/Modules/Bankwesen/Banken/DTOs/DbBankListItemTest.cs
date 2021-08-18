using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Bankwesen.Banken
{
    internal class DbBankListItemTest : IDbBankListItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public static void AssertDbDefault(IDbBankListItem dbBankListItem)
        {
            Assert.AreEqual(BankTestValues.IdDbDefault, dbBankListItem.Id);
            Assert.AreEqual(BankTestValues.NameDbDefault, dbBankListItem.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmDbDefault, dbBankListItem.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteDbDefault, dbBankListItem.IsPleite);
        }

        public static void AssertDbDefault2(IDbBankListItem dbBankListItem)
        {
            Assert.AreEqual(BankTestValues.IdDbDefault2, dbBankListItem.Id);
            Assert.AreEqual(BankTestValues.NameDbDefault2, dbBankListItem.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmDbDefault2, dbBankListItem.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteDbDefault2, dbBankListItem.IsPleite);
        }
    }
}