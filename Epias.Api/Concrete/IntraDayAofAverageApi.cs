using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Epias.Entities.Models;
using Epias.Transparency.Api.Interfaces;

namespace Epias.Transparency.Api.Concrete;

public class IntraDayAofAverageApi : IIntraDayAofAverageApi
{
    private readonly IHttpClientManager _httpClientManager;
    public IntraDayAofAverageApi(IHttpClientManager httpClientManager)
    {
        _httpClientManager = httpClientManager;
    }
    public async Task<List<IntraDayAofAverage>> GetAll()
    {
        string endDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                         DateTime.Now.Day.ToString();
        string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                           DateTime.Now.Day.ToString();

        var response = await _httpClientManager
            .GetResponseWithRetries(
                $"market/intra-day-aof-average?endDate={endDate}&DAILY&startDate={startDate}", "epiastransparency");

        List<IntraDayAofAverage> intraDayAofAveragess = new List<IntraDayAofAverage>();

        dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
        var list = obj.body.idmAofList;

        foreach (var intraDayAofAverage in list)
        {
            intraDayAofAveragess.Add(new IntraDayAofAverage()
            {
                Date = DateTime.Parse(intraDayAofAverage.date.ToString()),
                Aof = Decimal.Parse(intraDayAofAverage.aof.ToString()),
                Period = Int32.Parse(intraDayAofAverage.period.ToString()),
                PeriodType = intraDayAofAverage.periodType.ToString()
            });
        }
        return intraDayAofAveragess;
    }
}



