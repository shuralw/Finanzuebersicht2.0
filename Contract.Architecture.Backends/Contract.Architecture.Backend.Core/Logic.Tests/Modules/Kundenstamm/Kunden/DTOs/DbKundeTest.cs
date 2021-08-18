using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Contract.Persistence.Tools.Pagination;
using Contract.Architecture.Backend.Core.Logic.Tests.Tools.Pagination;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden
{
    internal class DbKundeTest : IDbKunde
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public Guid BankId { get; set; }

        public static IDbKunde Default()
        {
            return new DbKundeTest()
            {
                Id = KundeTestValues.IdDefault,
                Name = KundeTestValues.NameDefault,
                Balance = KundeTestValues.BalanceDefault,
                BankId = KundeTestValues.BankIdDefault,
            };
        }

        public static IDbKunde Default2()
        {
            return new DbKundeTest()
            {
                Id = KundeTestValues.IdDefault2,
                Name = KundeTestValues.NameDefault2,
                Balance = KundeTestValues.BalanceDefault2,
                BankId = KundeTestValues.BankIdDefault2,
            };
        }

        public static IDbKunde ForUpdate()
        {
            return new DbKundeTest()
            {
                Id = KundeTestValues.IdDefault,
                Name = KundeTestValues.NameForUpdate,
                Balance = KundeTestValues.BalanceForUpdate,
                BankId = KundeTestValues.BankIdForUpdate,
            };
        }

        public static void AssertDefault(IDbKunde dbKunde)
        {
            Assert.AreEqual(KundeTestValues.IdDefault, dbKunde.Id);
            Assert.AreEqual(KundeTestValues.NameDefault, dbKunde.Name);
            Assert.AreEqual(KundeTestValues.BalanceDefault, dbKunde.Balance);
            Assert.AreEqual(KundeTestValues.BankIdDefault, dbKunde.BankId);
        }

        public static void AssertDefault2(IDbKunde dbKunde)
        {
            Assert.AreEqual(KundeTestValues.IdDefault2, dbKunde.Id);
            Assert.AreEqual(KundeTestValues.NameDefault2, dbKunde.Name);
            Assert.AreEqual(KundeTestValues.BalanceDefault2, dbKunde.Balance);
            Assert.AreEqual(KundeTestValues.BankIdDefault2, dbKunde.BankId);
        }

        public static void AssertCreated(IDbKunde dbKunde)
        {
            Assert.AreEqual(KundeTestValues.IdForCreate, dbKunde.Id);
            Assert.AreEqual(KundeTestValues.NameForCreate, dbKunde.Name);
            Assert.AreEqual(KundeTestValues.BalanceForCreate, dbKunde.Balance);
            Assert.AreEqual(KundeTestValues.BankIdForCreate, dbKunde.BankId);
        }

        public static void AssertUpdated(IDbKunde dbKunde)
        {
            Assert.AreEqual(KundeTestValues.IdDefault, dbKunde.Id);
            Assert.AreEqual(KundeTestValues.NameForUpdate, dbKunde.Name);
            Assert.AreEqual(KundeTestValues.BalanceForUpdate, dbKunde.Balance);
            Assert.AreEqual(KundeTestValues.BankIdForUpdate, dbKunde.BankId);
        }
    }
}