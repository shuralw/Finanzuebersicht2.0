using System;

namespace Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden
{
    public interface IKundeCreate
    {
        string Name { get; set; }

        int Balance { get; set; }

        Guid BankId { get; set; }
    }
}