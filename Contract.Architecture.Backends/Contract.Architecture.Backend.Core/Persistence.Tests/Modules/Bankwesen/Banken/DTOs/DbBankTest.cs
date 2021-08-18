using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Bankwesen.Banken
{
    internal class DbBankTest : IDbBank
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public static IDbBank DbDefault()
        {
            return new DbBankTest()
            {
                Id = BankTestValues.IdDbDefault,
                Name = BankTestValues.NameDbDefault,
                EroeffnetAm = BankTestValues.EroeffnetAmDbDefault,
                IsPleite = BankTestValues.IsPleiteDbDefault,
            };
        }

        public static IDbBank DbDefault2()
        {
            return new DbBankTest()
            {
                Id = BankTestValues.IdDbDefault2,
                Name = BankTestValues.NameDbDefault2,
                EroeffnetAm = BankTestValues.EroeffnetAmDbDefault2,
                IsPleite = BankTestValues.IsPleiteDbDefault2,
            };
        }

        public static IDbBank ForCreate()
        {
            return new DbBankTest()
            {
                Id = BankTestValues.IdForCreate,
                Name = BankTestValues.NameForCreate,
                EroeffnetAm = BankTestValues.EroeffnetAmForCreate,
                IsPleite = BankTestValues.IsPleiteForCreate,
            };
        }

        public static IDbBank ForUpdate()
        {
            return new DbBankTest()
            {
                Id = BankTestValues.IdDbDefault,
                Name = BankTestValues.NameForUpdate,
                EroeffnetAm = BankTestValues.EroeffnetAmForUpdate,
                IsPleite = BankTestValues.IsPleiteForUpdate,
            };
        }

        public static void AssertDbDefault(IDbBank dbBank)
        {
            Assert.AreEqual(BankTestValues.IdDbDefault, dbBank.Id);
            Assert.AreEqual(BankTestValues.NameDbDefault, dbBank.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmDbDefault, dbBank.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteDbDefault, dbBank.IsPleite);
        }

        public static void AssertDbDefault2(IDbBank dbBank)
        {
            Assert.AreEqual(BankTestValues.IdDbDefault2, dbBank.Id);
            Assert.AreEqual(BankTestValues.NameDbDefault2, dbBank.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmDbDefault2, dbBank.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteDbDefault2, dbBank.IsPleite);
        }

        public static void AssertForCreate(IDbBank dbBank)
        {
            Assert.AreEqual(BankTestValues.IdForCreate, dbBank.Id);
            Assert.AreEqual(BankTestValues.NameForCreate, dbBank.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmForCreate, dbBank.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteForCreate, dbBank.IsPleite);
        }

        public static void AssertForUpdate(IDbBank dbBank)
        {
            Assert.AreEqual(BankTestValues.IdDbDefault, dbBank.Id);
            Assert.AreEqual(BankTestValues.NameForUpdate, dbBank.Name);
            Assert.AreEqual(BankTestValues.EroeffnetAmForUpdate, dbBank.EroeffnetAm);
            Assert.AreEqual(BankTestValues.IsPleiteForUpdate, dbBank.IsPleite);
        }
    }
}