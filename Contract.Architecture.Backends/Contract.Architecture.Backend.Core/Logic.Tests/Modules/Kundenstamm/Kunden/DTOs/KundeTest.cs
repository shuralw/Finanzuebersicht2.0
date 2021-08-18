using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden
{
    internal class KundeTest : IKunde
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public Guid BankId { get; set; }

        public static IKunde Default()
        {
            return new KundeTest()
            {
                Id = KundeTestValues.IdDefault,
                Name = KundeTestValues.NameDefault,
                Balance = KundeTestValues.BalanceDefault,
                BankId = KundeTestValues.BankIdDefault,
            };
        }

        public static IKunde Default2()
        {
            return new KundeTest()
            {
                Id = KundeTestValues.IdDefault2,
                Name = KundeTestValues.NameDefault2,
                Balance = KundeTestValues.BalanceDefault2,
                BankId = KundeTestValues.BankIdDefault2,
            };
        }

        public static void AssertDefault(IKunde kunde)
        {
            Assert.AreEqual(KundeTestValues.IdDefault, kunde.Id);
            Assert.AreEqual(KundeTestValues.NameDefault, kunde.Name);
            Assert.AreEqual(KundeTestValues.BalanceDefault, kunde.Balance);
            Assert.AreEqual(KundeTestValues.BankIdDefault, kunde.BankId);
        }

        public static void AssertDefault2(IKunde kunde)
        {
            Assert.AreEqual(KundeTestValues.IdDefault2, kunde.Id);
            Assert.AreEqual(KundeTestValues.NameDefault2, kunde.Name);
            Assert.AreEqual(KundeTestValues.BalanceDefault2, kunde.Balance);
            Assert.AreEqual(KundeTestValues.BankIdDefault2, kunde.BankId);
        }
    }
}