using System;
using System.Threading;
using System.Threading.Tasks;
using Epias.Api;
using Epias.Services.Interfaces;
using Epias.Transparency.Api.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Epias.BackgroundServices;

public class IntraDayIncomeBackgroundService : IHostedService, IDisposable
{
    private Timer _timer = null!;
    private readonly IServiceProvider _serviceProvider;
    private readonly IIntraDayIncomeApi _apiOperations;

    public IntraDayIncomeBackgroundService(IServiceProvider serviceProvider, IIntraDayIncomeApi apiOperations)
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
        var intraDayIncomes =await  _apiOperations.GetAll();
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<IIntraDayIncomeService>();
            var result = await context.AddListAsync(intraDayIncomes);
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

