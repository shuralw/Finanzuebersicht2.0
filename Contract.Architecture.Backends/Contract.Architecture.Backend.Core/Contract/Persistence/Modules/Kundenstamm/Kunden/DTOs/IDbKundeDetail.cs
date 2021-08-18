using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken;
using System;

namespace Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden
{
    public interface IDbKundeDetail
    {
        Guid Id { get; set; }

        string Name { get; set; }

        int Balance { get; set; }

        IDbBank Bank { get; set; }
    }
}