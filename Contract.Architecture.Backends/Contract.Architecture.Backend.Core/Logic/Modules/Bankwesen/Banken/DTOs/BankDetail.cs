using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Modules.Bankwesen.Banken
{
    internal class BankDetail : IBankDetail
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public IKunde Kunde { get; set; }

        internal static IBankDetail FromDbBankDetail(IDbBankDetail dbBankDetail)
        {
            return new BankDetail()
            {
                Id = dbBankDetail.Id,
                Name = dbBankDetail.Name,
                EroeffnetAm = dbBankDetail.EroeffnetAm,
                IsPleite = dbBankDetail.IsPleite,
                Kunde = Kundenstamm.Kunden.Kunde.FromDbKunde(dbBankDetail.Kunde),
            };
        }
    }
}