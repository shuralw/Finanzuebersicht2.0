using System;

namespace Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Time
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}