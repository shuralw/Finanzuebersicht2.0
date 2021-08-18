using Contract.Architecture.Backend.Core.Persistence.Modules.Kundenstamm.Kunden;
using System;
using System.Collections.Generic;

namespace Contract.Architecture.Backend.Core.Persistence.Modules.Bankwesen.Banken
{
    public class EfBank
    {
        public EfBank()
        {
            this.Kunden = new HashSet<EfKunde>();
        }

        public Guid Id { get; set; }

        public Guid MandantId { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public virtual ICollection<EfKunde> Kunden { get; set; }
    }
}