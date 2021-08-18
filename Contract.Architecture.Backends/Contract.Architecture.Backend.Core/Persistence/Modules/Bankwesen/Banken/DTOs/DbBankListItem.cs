using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Persistence.Modules.Kundenstamm.Kunden;
using System;

namespace Contract.Architecture.Backend.Core.Persistence.Modules.Bankwesen.Banken
{
    internal class DbBankListItem : IDbBankListItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public IDbKunde Kunde { get; set; }

        internal static IDbBankListItem FromEfBank(EfBank efBank)
        {
            if (efBank == null)
            {
                return null;
            }

            return new DbBankListItem()
            {
                Id = efBank.Id,
                Name = efBank.Name,
                EroeffnetAm = efBank.EroeffnetAm,
                IsPleite = efBank.IsPleite,
                Kunde = DbKunde.FromEfKunde(efBank.Kunde),
            };
        }
    }
}