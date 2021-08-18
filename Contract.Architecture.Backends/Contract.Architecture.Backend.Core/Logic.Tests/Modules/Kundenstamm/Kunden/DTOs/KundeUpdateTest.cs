using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden
{
    internal class KundeUpdateTest : IKundeUpdate
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public Guid BankId { get; set; }

        public static IKundeUpdate ForUpdate()
        {
            return new KundeUpdateTest()
            {
                Id = KundeTestValues.IdDefault,
                Name = KundeTestValues.NameForUpdate,
                Balance = KundeTestValues.BalanceForUpdate,
                BankId = KundeTestValues.BankIdForUpdate,
            };
        }
    }
}