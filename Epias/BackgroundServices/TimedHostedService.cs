using System;
using System.Threading;
using System.Threading.Tasks;
using Epias.Api;
using Epias.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Epias.BackgroundServices;

public class TimedHostedService : IHostedService, IDisposable
{
    private Timer _timer = null!;
    private readonly IServiceProvider _serviceProvider;
    private readonly IApiOperations _apiOperations;

    public TimedHostedService(IServiceProvider serviceProvider, IApiOperations apiOperations)
    {
        _apiOperations = apiOperations;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(50));

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        var tradeHistories = _apiOperations.GetTradeHistories();
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ITradeHistoryService>();
            context.AddList(tradeHistories);
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

