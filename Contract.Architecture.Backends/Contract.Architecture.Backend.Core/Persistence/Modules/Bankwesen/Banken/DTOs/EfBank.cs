using Contract.Architecture.Backend.Core.Persistence.Modules.Kundenstamm.Kunden;
using System;
using System.Collections.Generic;

namespace Contract.Architecture.Backend.Core.Persistence.Modules.Bankwesen.Banken
{
    public class EfBank
    {
        public EfBank()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }

        public virtual EfKunde Kunde { get; set; }
    }
}