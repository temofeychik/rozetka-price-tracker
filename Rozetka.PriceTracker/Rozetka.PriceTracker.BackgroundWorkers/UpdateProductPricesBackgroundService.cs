using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rozetka.PriceTracker.Services.ProductUpdater;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.BackgroundWorkers
{
    public class UpdateProductPricesBackgroundService : BackgroundService
    {
        public const int IntervalBetweenPriceNeedToUpdateInMinutes = 60;
        public const int UpdatePricesWorkIntervalInSeconds = 60;

        private readonly ILogger<UpdateProductPricesBackgroundService> _logger;
        private readonly IServiceProvider _services;
        public UpdateProductPricesBackgroundService(ILogger<UpdateProductPricesBackgroundService> logger,
            IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Update products background service is starting.");
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                using (var scope = _services.CreateScope())
                {
                    var productUpdaterService =
                        scope.ServiceProvider
                            .GetRequiredService<IProductUpdaterService>();

                    await productUpdaterService.UpdatePrices(TimeSpan.FromMinutes(IntervalBetweenPriceNeedToUpdateInMinutes));

                    await Task.Delay(TimeSpan.FromSeconds(UpdatePricesWorkIntervalInSeconds), stoppingToken);
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Update products background service is stopping.");

            await Task.CompletedTask;
        }

    }
}
