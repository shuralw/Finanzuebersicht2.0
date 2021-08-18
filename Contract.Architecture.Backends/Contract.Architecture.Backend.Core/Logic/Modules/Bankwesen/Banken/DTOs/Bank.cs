using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Modules.Bankwesen.Banken
{
    internal class Bank : IBank
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        internal static void UpdateDbBank(IDbBank dbBank, IBankUpdate bankUpdate)
        {
            dbBank.Name = bankUpdate.Name;
            dbBank.EroeffnetAm = bankUpdate.EroeffnetAm;
            dbBank.IsPleite = bankUpdate.IsPleite;
        }

        internal static IBank FromDbBank(IDbBank dbBank)
        {
            return new Bank()
            {
                Id = dbBank.Id,
                Name = dbBank.Name,
                EroeffnetAm = dbBank.EroeffnetAm,
                IsPleite = dbBank.IsPleite,
            };
        }

        internal static IDbBank CreateDbBank(Guid bankId, IBankCreate bankCreate)
        {
            return new DbBank()
            {
                Id = bankId,
                Name = bankCreate.Name,
                EroeffnetAm = bankCreate.EroeffnetAm,
                IsPleite = bankCreate.IsPleite,
            };
        }
    }
}