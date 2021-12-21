﻿using System.Threading.Tasks;

namespace Finanzuebersicht.Backend.Core.Contract.Logic.JobScheduler
{
    public interface IScheduledJob
    {
        int GetDelayInSeconds();

        bool IsExecutingOnInitialization();

        Task Execute();
    }
}