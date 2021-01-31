using Contract.Architecture.Contract.Logic.Services.Identifier;
using System;

namespace Contract.Architecture.Logic.Services.Identifier
{
    internal class GuidGenerator : IGuidGenerator
    {
        public Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }
}