using System;

namespace Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Identifier
{
    public interface IGuidGenerator
    {
        Guid NewGuid();
    }
}