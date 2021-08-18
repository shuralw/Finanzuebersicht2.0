using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Kundenstamm.Kunden
{
    internal class DbKundeTest : IDbKunde
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public Guid BankId { get; set; }

        public static IDbKunde DbDefault()
        {
            return new DbKundeTest()
            {
                Id = KundeTestValues.IdDbDefault,
                Name = KundeTestValues.NameDbDefault,
                Balance = KundeTestValues.BalanceDbDefault,
                BankId = KundeTestValues.BankIdDbDefault,
            };
        }

        public static IDbKunde DbDefault2()
        {
            return new DbKundeTest()
            {
                Id = KundeTestValues.IdDbDefault2,
                Name = KundeTestValues.NameDbDefault2,
                Balance = KundeTestValues.BalanceDbDefault2,
                BankId = KundeTestValues.BankIdDbDefault2,
            };
        }

        public static IDbKunde ForCreate()
        {
            return new DbKundeTest()
            {
                Id = KundeTestValues.IdForCreate,
                Name = KundeTestValues.NameForCreate,
                Balance = KundeTestValues.BalanceForCreate,
                BankId = KundeTestValues.BankIdForCreate,
            };
        }

        public static IDbKunde ForUpdate()
        {
            return new DbKundeTest()
            {
                Id = KundeTestValues.IdDbDefault,
                Name = KundeTestValues.NameForUpdate,
                Balance = KundeTestValues.BalanceForUpdate,
                BankId = KundeTestValues.BankIdForUpdate,
            };
        }

        public static void AssertDbDefault(IDbKunde dbKunde)
        {
            Assert.AreEqual(KundeTestValues.IdDbDefault, dbKunde.Id);
            Assert.AreEqual(KundeTestValues.NameDbDefault, dbKunde.Name);
            Assert.AreEqual(KundeTestValues.BalanceDbDefault, dbKunde.Balance);
            Assert.AreEqual(KundeTestValues.BankIdDbDefault, dbKunde.BankId);
        }

        public static void AssertDbDefault2(IDbKunde dbKunde)
        {
            Assert.AreEqual(KundeTestValues.IdDbDefault2, dbKunde.Id);
            Assert.AreEqual(KundeTestValues.NameDbDefault2, dbKunde.Name);
            Assert.AreEqual(KundeTestValues.BalanceDbDefault2, dbKunde.Balance);
            Assert.AreEqual(KundeTestValues.BankIdDbDefault2, dbKunde.BankId);
        }

        public static void AssertForCreate(IDbKunde dbKunde)
        {
            Assert.AreEqual(KundeTestValues.IdForCreate, dbKunde.Id);
            Assert.AreEqual(KundeTestValues.NameForCreate, dbKunde.Name);
            Assert.AreEqual(KundeTestValues.BalanceForCreate, dbKunde.Balance);
            Assert.AreEqual(KundeTestValues.BankIdForCreate, dbKunde.BankId);
        }

        public static void AssertForUpdate(IDbKunde dbKunde)
        {
            Assert.AreEqual(KundeTestValues.IdDbDefault, dbKunde.Id);
            Assert.AreEqual(KundeTestValues.NameForUpdate, dbKunde.Name);
            Assert.AreEqual(KundeTestValues.BalanceForUpdate, dbKunde.Balance);
            Assert.AreEqual(KundeTestValues.BankIdForUpdate, dbKunde.BankId);
        }
    }
}