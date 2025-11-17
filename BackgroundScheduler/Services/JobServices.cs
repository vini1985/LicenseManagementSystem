using IntervalJob.Models.Interfaces;

namespace IntervalJob.Models.Services
{
    public class JobServices : IJobTask
    {
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("JobServices is running the job task.");
        }
    }
}
