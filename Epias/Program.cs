using Epias.Api;
using Epias.BackgroundServices;
using Epias.Data.Abstract;
using Epias.Data.Concrete.EntityFramework.Contexts;
using Epias.Data.Concrete.EntityFramework.Repostories;
using Epias.Services.Abstract;
using Epias.Services.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ITradeHistoryRepository, EfTradeHistoryRepository>();
builder.Services.AddTransient<ITradeHistoryService, TradeHistoryManager>();
builder.Services.AddTransient<IApiOperations, ApiOperations>();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EpiasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(EpiasContext).Assembly.FullName)));
builder.Services.AddHostedService<TimedHostedService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
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
