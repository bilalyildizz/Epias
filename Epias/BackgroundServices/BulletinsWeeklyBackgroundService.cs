using Epias.Services.Interfaces;
using Epias.Transparency.Api.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Epias.BackgroundServices
{
    public class BulletinsWeeklyBackgroundService : IHostedService, IDisposable
    {
        private Timer _timer = null!;
        private readonly IServiceProvider _serviceProvider;
        private readonly IBulletinsWeeklyApi _apiOperations;

        public BulletinsWeeklyBackgroundService(IServiceProvider serviceProvider, IBulletinsWeeklyApi apiOperations)
        {
            _apiOperations = apiOperations;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            var bulletinsWeeklies = await _apiOperations.GetAll();
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IBulletinsWeeklyService>();
                var result = await context.AddListAsync(bulletinsWeeklies);
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
