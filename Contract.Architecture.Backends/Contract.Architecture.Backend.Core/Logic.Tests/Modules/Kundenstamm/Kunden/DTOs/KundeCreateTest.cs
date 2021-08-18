using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden
{
    internal class KundeCreateTest : IKundeCreate
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public Guid BankId { get; set; }

        public static IKundeCreate ForCreate()
        {
            return new KundeCreateTest()
            {
                Id = KundeTestValues.IdForCreate,
                Name = KundeTestValues.NameForCreate,
                Balance = KundeTestValues.BalanceForCreate,
                BankId = KundeTestValues.BankIdForCreate,
            };
        }
    }
}