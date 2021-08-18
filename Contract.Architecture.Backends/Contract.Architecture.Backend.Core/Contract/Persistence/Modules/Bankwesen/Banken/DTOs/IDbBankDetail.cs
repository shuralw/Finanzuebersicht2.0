using Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden;
using System;

namespace Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Bankwesen.Banken
{
    public interface IDbBankDetail
    {
        Guid Id { get; set; }

        string Name { get; set; }

        DateTime EroeffnetAm { get; set; }

        bool IsPleite { get; set; }

        IDbKunde Kunde { get; set; }
    }
}