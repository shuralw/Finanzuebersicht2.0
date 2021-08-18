using System;

namespace Contract.Architecture.Backend.Core.Contract.Persistence.Modules.Kundenstamm.Kunden
{
    public interface IDbKunde
    {
        Guid Id { get; set; }

        string Name { get; set; }

        int Balance { get; set; }

        Guid BankId { get; set; }
    }
}