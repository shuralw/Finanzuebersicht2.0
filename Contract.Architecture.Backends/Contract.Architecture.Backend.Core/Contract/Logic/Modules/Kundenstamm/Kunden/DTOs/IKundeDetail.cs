using Contract.Architecture.Backend.Core.Contract.Logic.Modules.Bankwesen.Banken;
using System;

namespace Contract.Architecture.Backend.Core.Contract.Logic.Modules.Kundenstamm.Kunden
{
    public interface IKundeDetail
    {
        Guid Id { get; set; }

        string Name { get; set; }

        int Balance { get; set; }

        IBank Bank { get; set; }
    }
}