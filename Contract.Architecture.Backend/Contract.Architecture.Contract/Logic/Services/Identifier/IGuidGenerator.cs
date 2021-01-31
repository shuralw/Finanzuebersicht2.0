using System;

namespace Contract.Architecture.Contract.Logic.Services.Identifier
{
    public interface IGuidGenerator
    {
        Guid NewGuid();
    }
}