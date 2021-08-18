using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using System;

namespace Contract.Architecture.Backend.Core.Persistence.Modules.Kundenstamm.Kunden
{
    internal class DbKunde : IDbKunde
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public Guid BankId { get; set; }

        internal static void UpdateEfKunde(EfKunde efKunde, IDbKunde dbKunde)
        {
            efKunde.Name = dbKunde.Name;
            efKunde.Balance = dbKunde.Balance;
            efKunde.BankId = dbKunde.BankId;
        }

        internal static IDbKunde FromEfKunde(EfKunde efKunde)
        {
            if (efKunde == null)
            {
                return null;
            }

            return new DbKunde()
            {
                Id = efKunde.Id,
                Name = efKunde.Name,
                Balance = efKunde.Balance,
                BankId = efKunde.BankId,
            };
        }

        internal static EfKunde ToEfKunde(IDbKunde dbKunde)
        {
            return new EfKunde()
            {
                Id = dbKunde.Id,
                Name = dbKunde.Name,
                Balance = dbKunde.Balance,
                BankId = dbKunde.BankId,
            };
        }
    }
}