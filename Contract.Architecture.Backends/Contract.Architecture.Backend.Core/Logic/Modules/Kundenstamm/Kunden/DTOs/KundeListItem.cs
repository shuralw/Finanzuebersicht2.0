using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Modules.Kundenstamm.Kunden
{
    internal class KundeListItem : IKundeListItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public IBank Bank { get; set; }

        internal static IKundeListItem FromDbKundeListItem(IDbKundeListItem dbKundeListItem)
        {
            return new KundeListItem()
            {
                Id = dbKundeListItem.Id,
                Name = dbKundeListItem.Name,
                Balance = dbKundeListItem.Balance,
                Bank = Bankwesen.Banken.Bank.FromDbBank(dbKundeListItem.Bank),
            };
        }
    }
}