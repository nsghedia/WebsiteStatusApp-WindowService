namespace WebsiteStatusApp
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient client;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client.Dispose();
            return base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await client.GetAsync("https://www.iamtimcorey.com/");

                if (result.IsSuccessStatusCode)
                    _logger.LogInformation("The website is up. status code : {statuscode}", result.StatusCode);
                else
                    _logger.LogError("The website is down. status code : {statuscode}", result.StatusCode);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}