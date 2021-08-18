using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Logic.Modules.Kundenstamm.Kunden;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Architecture.Backend.Core.Logic.Modules.Bankwesen.Banken
{
    internal class BankDetail : IBankDetail
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public IEnumerable<IKunde> Kunden { get; set; }

        internal static IBankDetail FromDbBankDetail(IDbBankDetail dbBankDetail)
        {
            return new BankDetail()
            {
                Id = dbBankDetail.Id,
                Name = dbBankDetail.Name,
                EroeffnetAm = dbBankDetail.EroeffnetAm,
                IsPleite = dbBankDetail.IsPleite,
                Kunden = dbBankDetail.Kunden.Select(dbKunde => Kunde.FromDbKunde(dbKunde)),
            };
        }
    }
}