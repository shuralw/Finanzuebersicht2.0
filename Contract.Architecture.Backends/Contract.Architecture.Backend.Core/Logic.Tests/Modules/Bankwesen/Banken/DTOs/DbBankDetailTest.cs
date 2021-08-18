using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken
{
    internal class DbBankDetailTest : IDbBankDetail
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public IDbKunde Kunde { get; set; }

        public static IDbBankDetail Default()
        {
            return new DbBankDetailTest()
            {
                Id = BankTestValues.IdDefault,
                Name = BankTestValues.NameDefault,
                EroeffnetAm = BankTestValues.EroeffnetAmDefault,
                IsPleite = BankTestValues.IsPleiteDefault,
                Kunde = DbKundeTest.Default(),
            };
        }

        public static IDbBankDetail Default2()
        {
            return new DbBankDetailTest()
            {
                Id = BankTestValues.IdDefault,
                Name = BankTestValues.NameDefault2,
                EroeffnetAm = BankTestValues.EroeffnetAmDefault2,
                IsPleite = BankTestValues.IsPleiteDefault2,
                Kunde = DbKundeTest.Default2(),
            };
        }

        public static void AssertDefault(IDbBankDetail dbBankDetail)
        {
            Assert.AreEqual(BankTestValues.IdDefault, dbBankDetail.Id);
            DbKundeTest.AssertDefault(dbBankDetail.Kunde);
        }

        public static void AssertDefault2(IDbBankDetail dbBankDetail)
        {
            Assert.AreEqual(BankTestValues.IdDefault2, dbBankDetail.Id);
            Assert.AreEqual(BankTestValues.NameDefault2, dbBankDetail.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmDefault2, dbBankDetail.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteDefault2, dbBankDetail.IsPleite);
            DbKundeTest.AssertDefault2(dbBankDetail.Kunde);
        }
    }
}