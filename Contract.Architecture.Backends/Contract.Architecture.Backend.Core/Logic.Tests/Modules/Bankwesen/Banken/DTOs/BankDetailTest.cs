using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken
{
    internal class BankDetailTest : IBankDetail
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public IEnumerable<IKunde> Kunden { get; set; }

        public static IBankDetail Default()
        {
            return new BankDetailTest()
            {
                Id = BankTestValues.IdDefault,
                Name = BankTestValues.NameDefault,
                EroeffnetAm = BankTestValues.EroeffnetAmDefault,
                IsPleite = BankTestValues.IsPleiteDefault,
            };
        }

        public static IBankDetail Default2()
        {
            return new BankDetailTest()
            {
                Id = BankTestValues.IdDefault2,
                Name = BankTestValues.NameDefault2,
                EroeffnetAm = BankTestValues.EroeffnetAmDefault2,
                IsPleite = BankTestValues.IsPleiteDefault2,
            };
        }

        public static void AssertDefault(IBankDetail bankDetail)
        {
            Assert.AreEqual(BankTestValues.IdDefault, bankDetail.Id);
            Assert.AreEqual(BankTestValues.NameDefault, bankDetail.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmDefault, bankDetail.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteDefault, bankDetail.IsPleite);
            KundeTest.AssertDefault(bankDetail.Kunden.ToArray()[0]);
        }

        public static void AssertDefault2(IBankDetail bankDetail)
        {
            Assert.AreEqual(BankTestValues.IdDefault2, bankDetail.Id);
            Assert.AreEqual(BankTestValues.NameDefault2, bankDetail.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmDefault2, bankDetail.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteDefault2, bankDetail.IsPleite);
            KundeTest.AssertDefault2(bankDetail.Kunden.ToArray()[0]);
        }
    }
}