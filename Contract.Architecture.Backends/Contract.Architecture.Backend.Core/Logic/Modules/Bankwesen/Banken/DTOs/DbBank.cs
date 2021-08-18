using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Modules.Bankwesen.Banken
{
    internal class DbBank : IDbBank
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EroeffnetAm { get; set; }

        public bool IsPleite { get; set; }
    }
}