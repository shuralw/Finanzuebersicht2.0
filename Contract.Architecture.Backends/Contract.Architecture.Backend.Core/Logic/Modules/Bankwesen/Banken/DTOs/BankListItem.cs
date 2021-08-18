using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Modules.Bankwesen.Banken
{
    internal class BankListItem : IBankListItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public IKunde Kunde { get; set; }

        internal static IBankListItem FromDbBankListItem(IDbBankListItem dbBankListItem)
        {
            return new BankListItem()
            {
                Id = dbBankListItem.Id,
                Name = dbBankListItem.Name,
                EroeffnetAm = dbBankListItem.EroeffnetAm,
                IsPleite = dbBankListItem.IsPleite,
                Kunde = Kundenstamm.Kunden.Kunde.FromDbKunde(dbBankListItem.Kunde),
            };
        }
    }
}