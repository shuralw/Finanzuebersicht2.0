using System;

namespace Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken
{
    public interface IBankListItem
    {
        Guid Id { get; set; }

        string Name { get; set; }

        DateTime EroeffnetAm { get; set; }

        bool IsPleite { get; set; }
    }
}