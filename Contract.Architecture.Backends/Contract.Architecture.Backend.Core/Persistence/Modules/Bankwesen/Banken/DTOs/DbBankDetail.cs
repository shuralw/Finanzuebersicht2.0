using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Persistence.Modules.Kundenstamm.Kunden;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Architecture.Backend.Core.Persistence.Modules.Bankwesen.Banken
{
    internal class DbBankDetail : IDbBankDetail
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public IEnumerable<IDbKunde> Kunden { get; set; }

        internal static IDbBankDetail FromEfBank(EfBank efBank)
        {
            if (efBank == null)
            {
                return null;
            }

            return new DbBankDetail()
            {
                Id = efBank.Id,
                Name = efBank.Name,
                EroeffnetAm = efBank.EroeffnetAm,
                IsPleite = efBank.IsPleite,
                Kunden = efBank.Kunden.Select(efKunde => DbKunde.FromEfKunde(efKunde)),
            };
        }
    }
}