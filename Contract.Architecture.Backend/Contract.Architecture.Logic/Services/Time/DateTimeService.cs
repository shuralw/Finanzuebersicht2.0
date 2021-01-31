using Contract.Architecture.Contract.Logic.Services.Time;
using System;

namespace Contract.Architecture.Logic.Services.Time
{
    internal class DateTimeService : IDateTimeService
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}