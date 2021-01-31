using System.Threading.Tasks;

namespace Contract.Architecture.Contract.Logic.Services.ScheduledJob
{
    public interface IScheduledJob
    {
        int GetDelayInSeconds();

        bool IsExecutingOnInitialization();

        Task Execute();
    }
}