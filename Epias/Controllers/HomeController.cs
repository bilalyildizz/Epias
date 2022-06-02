using Epias.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Epias.Data.Abstract;
using Epias.Services.Interfaces;


namespace Epias.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IIntraDayTradeHistoryService _tradeHistoryService;

    public HomeController(ILogger<HomeController> logger, IIntraDayTradeHistoryService tradeHistoryService)
    {
        _logger = logger;
        _tradeHistoryService = tradeHistoryService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _tradeHistoryService.GetAllByDateAsync();

        return View(result.Data);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

