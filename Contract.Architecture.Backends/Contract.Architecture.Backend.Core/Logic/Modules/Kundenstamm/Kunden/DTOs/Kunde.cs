using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Modules.Kundenstamm.Kunden
{
    internal class Kunde : IKunde
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public Guid BankId { get; set; }

        internal static void UpdateDbKunde(IDbKunde dbKunde, IKundeUpdate kundeUpdate)
        {
            dbKunde.Name = kundeUpdate.Name;
            dbKunde.Balance = kundeUpdate.Balance;
            dbKunde.BankId = kundeUpdate.BankId;
        }

        internal static IKunde FromDbKunde(IDbKunde dbKunde)
        {
            return new Kunde()
            {
                Id = dbKunde.Id,
                Name = dbKunde.Name,
                Balance = dbKunde.Balance,
                BankId = dbKunde.BankId,
            };
        }

        internal static IDbKunde CreateDbKunde(Guid kundeId, IKundeCreate kundeCreate)
        {
            return new DbKunde()
            {
                Id = kundeId,
                Name = kundeCreate.Name,
                Balance = kundeCreate.Balance,
                BankId = kundeCreate.BankId,
            };
        }
    }
}