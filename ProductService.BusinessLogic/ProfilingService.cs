using System.Threading.Tasks;
using System;
using System.Diagnostics;

namespace ProductService.BusinessLogic
{
    public interface IProfilingService
    {
        TimeSpan GetDuration(Action action);

        Task<TimeSpan> GetDurationAsync(Func<Task> actionAsync);
        
    }

    public class ProfilingService : IProfilingService
    {
        
        public TimeSpan GetDuration(Action action)
        {
            var timer = new Stopwatch();
            timer.Start();
            action();
            timer.Stop();
            return timer.Elapsed;
        }

        public async Task<TimeSpan> GetDurationAsync(Func<Task> actionAsync)
        {
            var timer = new Stopwatch();
            timer.Start();
            await actionAsync();
            timer.Stop();
            return timer.Elapsed;
        }
    }
}