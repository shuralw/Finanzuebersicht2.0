using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using System;

namespace Contract.Architecture.Backend.Core.Persistence.Modules.Bankwesen.Banken
{
    internal class DbBank : IDbBank
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        internal static void UpdateEfBank(EfBank efBank, IDbBank dbBank)
        {
            efBank.Name = dbBank.Name;
            efBank.EroeffnetAm = dbBank.EroeffnetAm;
            efBank.IsPleite = dbBank.IsPleite;
        }

        internal static IDbBank FromEfBank(EfBank efBank)
        {
            if (efBank == null)
            {
                return null;
            }

            return new DbBank()
            {
                Id = efBank.Id,
                Name = efBank.Name,
                EroeffnetAm = efBank.EroeffnetAm,
                IsPleite = efBank.IsPleite,
            };
        }

        internal static EfBank ToEfBank(IDbBank dbBank)
        {
            return new EfBank()
            {
                Id = dbBank.Id,
                Name = dbBank.Name,
                EroeffnetAm = dbBank.EroeffnetAm,
                IsPleite = dbBank.IsPleite,
            };
        }
    }
}