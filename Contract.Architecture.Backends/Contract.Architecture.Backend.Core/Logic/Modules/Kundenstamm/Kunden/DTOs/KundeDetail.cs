using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Modules.Kundenstamm.Kunden
{
    internal class KundeDetail : IKundeDetail
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public IBank Bank { get; set; }

        internal static IKundeDetail FromDbKundeDetail(IDbKundeDetail dbKundeDetail)
        {
            return new KundeDetail()
            {
                Id = dbKundeDetail.Id,
                Name = dbKundeDetail.Name,
                Balance = dbKundeDetail.Balance,
                Bank = Bankwesen.Banken.Bank.FromDbBank(dbKundeDetail.Bank),
            };
        }
    }
}