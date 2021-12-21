using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Identifier;
using System;

namespace Finanzuebersicht.Backend.Core.Logic.Tools.Identifier
{
    internal class GuidGenerator : IGuidGenerator
    {
        public Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }
}