using System;
using System.Threading;
using System.Threading.Tasks;
using Epias.Services.Interfaces;
using Epias.Transparency.Api.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Epias.BackgroundServices;

public class BulletinDailyBackgroundService : IHostedService, IDisposable
{
    private Timer _timer = null!;
    private readonly IServiceProvider _serviceProvider;
    private readonly IBulletinDailyApi _apiOperations;

    public BulletinDailyBackgroundService(IServiceProvider serviceProvider, IBulletinDailyApi apiOperations)
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
        var bulletinDailies = await _apiOperations.GetAll();
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<IBulletinDailyService>();
            var result = await context.AddListAsync(bulletinDailies);
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

