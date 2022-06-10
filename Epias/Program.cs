using System;
using Epias.Api;
using Epias.BackgroundServices;
using Epias.Data.Abstract;
using Epias.Data.Concrete.EntityFramework.Contexts;
using Epias.Data.Concrete.EntityFramework.Repostories;
using Epias.Data.Interfaces;
using Epias.Services.Interfaces;
using Epias.Services.Services;
using Epias.Transparency.Api;
using Epias.Transparency.Api.Concrete;
using Epias.Transparency.Api.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IIntraDayTradeHistoryRepository, EfIntraDayTradeHistoryRepository>();
builder.Services.AddTransient<IIntraDayTradeHistoryService, IntraDayTradeHistoryManager>();
builder.Services.AddTransient<IIntraDayTradeHistoryApi, IntraDayTradeHistoryApi>();

builder.Services.AddTransient<IIntraDayAofAverageRepository, EfIntraDayAofAverageRepository>();
builder.Services.AddTransient<IIntraDayAofAverageService, IntraDayAofAverageManager>();
builder.Services.AddTransient<IIntraDayAofAverageApi, IntraDayAofAverageApi>();

builder.Services.AddTransient<IIntraDayAofRepository, EfIntraDayAofRepository>();
builder.Services.AddTransient<IIntraDayAofService, IntraDayAofManager>();
builder.Services.AddTransient<IIntraDayAofApi, IntraDayAofApi>();

builder.Services.AddTransient<IIntraDayIncomeRepository, EfIntraDayIncomeRepository>();
builder.Services.AddTransient<IIntraDayIncomeService, IntraDayIncomeManager>();
builder.Services.AddTransient<IIntraDayIncomeApi, IntraDayIncomeApi>();

builder.Services.AddTransient<IIntraDayIncomeSummaryRepository, EfIntraDayIncomeSummaryRepository>();
builder.Services.AddTransient<IIntraDayIncomeSummaryService, IntraDayIncomeSummaryManager>();
builder.Services.AddTransient<IIntraDayIncomeSummaryApi, IntraDayIncomeSummaryApi>();

builder.Services.AddTransient<IIntraDaySummaryRepository, EfIntraDaySummaryRepository>();
builder.Services.AddTransient<IIntraDaySummaryService, IntraDaySummaryManager>();
builder.Services.AddTransient<IIntraDaySummaryApi, IntraDaySummaryApi>();

builder.Services.AddTransient<IIntraDayVolumeRepository, EfIntraDayVolumeRepository>();
builder.Services.AddTransient<IIntraDayVolumeService, IntraDayVolumeManager>();
builder.Services.AddTransient<IIntraDayVolumeApi, IntraDayVolumeApi>();

builder.Services.AddTransient<IIntraDayVolumeSummaryRepository, EfIntraDayVolumeSummaryRepository>();
builder.Services.AddTransient<IIntraDayVolumeSummaryService, IntraDayVolumeSummaryManager>();
builder.Services.AddTransient<IIntraDayVolumeSummaryApi, IntraDayVolumeSummaryApi>();

builder.Services.AddTransient<IMcpSmpRepository, EfMcpSmpRepository>();
builder.Services.AddTransient<IMcpSmpService, McpSmpManager>();
builder.Services.AddTransient<IMcpSmpApi, McpSmpApi>();

builder.Services.AddSingleton<IHttpClientManager, HttpClientManager>();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EpiasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient("epiastransparency", c =>
{
    c.BaseAddress = new Uri("https://seffaflik.epias.com.tr/transparency/service/");
    c.Timeout = TimeSpan.FromSeconds(20);
    c.DefaultRequestVersion = new Version(2, 0);
});

builder.Services.AddHostedService<IntraDayTradeHistoryBackgroundService>();
builder.Services.AddHostedService<IntraDayAofBackgroundService>();
builder.Services.AddHostedService<IntraDayAofAverageBackgroundService>();
builder.Services.AddHostedService<IntraDayIncomeBackgroundService>();
builder.Services.AddHostedService<IntraDayIncomeSummaryBackgroundService>();
builder.Services.AddHostedService<IntraDaySummaryBackgroundService>();
builder.Services.AddHostedService<IntraDayVolumeBackgroundService>();
builder.Services.AddHostedService<IntraDayVolumeSummaryBackgroundService>();
builder.Services.AddHostedService<McpSmpBackgroundService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) 
    .AllowCredentials()); 

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
