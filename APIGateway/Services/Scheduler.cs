using IntervalJob.Models.Interfaces;

namespace IntervalJob.Models.Services
{
    public class Scheduler : BackgroundService
    {
        private readonly ILogger<Scheduler> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;
        private readonly TimeSpan _interval;
        public Scheduler(ILogger<Scheduler> logger, IServiceScopeFactory scopeFactory,  IConfiguration configuration)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _interval = TimeSpan.FromMinutes(configuration.GetValue<int>("JobInterval:Minutes"));

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Interval job running at: {time}", DateTimeOffset.Now);
                using var scope = _scopeFactory.CreateScope();
                var job = scope.ServiceProvider.GetRequiredService<IJobTask>();
                await job.RunAsync(stoppingToken);
                await Task.Delay(_interval, stoppingToken);
            }
        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Interval job is stopping at: {time}", DateTimeOffset.Now);
            await base.StopAsync(stoppingToken);
        }
    }
}
