using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken
{
    internal class BankTest : IBank
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public static IBank Default()
        {
            return new BankTest()
            {
                Id = BankTestValues.IdDefault,
                Name = BankTestValues.NameDefault,
                EroeffnetAm = BankTestValues.EroeffnetAmDefault,
                IsPleite = BankTestValues.IsPleiteDefault,
            };
        }

        public static IBank Default2()
        {
            return new BankTest()
            {
                Id = BankTestValues.IdDefault2,
                Name = BankTestValues.NameDefault2,
                EroeffnetAm = BankTestValues.EroeffnetAmDefault2,
                IsPleite = BankTestValues.IsPleiteDefault2,
            };
        }

        public static void AssertDefault(IBank bank)
        {
            Assert.AreEqual(BankTestValues.IdDefault, bank.Id);
            Assert.AreEqual(BankTestValues.NameDefault, bank.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmDefault, bank.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteDefault, bank.IsPleite);
        }

        public static void AssertDefault2(IBank bank)
        {
            Assert.AreEqual(BankTestValues.IdDefault2, bank.Id);
            Assert.AreEqual(BankTestValues.NameDefault2, bank.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmDefault2, bank.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteDefault2, bank.IsPleite);
        }
    }
}