using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Time;
using System;

namespace Finanzuebersicht.Backend.Core.Logic.Tools.Time
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}