using Epias.Services.Interfaces;
using Epias.Transparency.Api.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Epias.BackgroundServices
{
    public class BulletinsMonthlyBackgroundService : IHostedService, IDisposable
    {
        private Timer _timer = null!;
        private readonly IServiceProvider _serviceProvider;
        private readonly IBulletinsMonthlyApi _apiOperations;

        public BulletinsMonthlyBackgroundService(IServiceProvider serviceProvider, IBulletinsMonthlyApi apiOperations)
        {
            
            _serviceProvider = serviceProvider;
            _apiOperations = apiOperations;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            var bulletinsMonthlies = await _apiOperations.GetAll();
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IBulletinsMonthlyService>();
                var result = await context.AddListAsync(bulletinsMonthlies);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
