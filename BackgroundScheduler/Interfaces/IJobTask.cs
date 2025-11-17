namespace IntervalJob.Models.Interfaces
{
    public interface IJobTask
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
