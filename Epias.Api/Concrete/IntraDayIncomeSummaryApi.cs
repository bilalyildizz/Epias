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

public class IntraDayIncomeSummaryApi : IIntraDayIncomeSummaryApi
{
    private readonly IHttpClientManager _httpClientManager;
    public IntraDayIncomeSummaryApi(IHttpClientManager httpClientManager)
    {
        _httpClientManager = httpClientManager;
    }

    public async Task<List<IntraDayIncomeSummary>> GetAll()
    {
        string endDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                         DateTime.Now.Day.ToString();
        string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                           DateTime.Now.Day.ToString();

        var response = await _httpClientManager
            .GetResponseWithRetries(
                $"market/intra-day-income-summary?endDate={endDate}&DAILY&startDate={startDate}", "epiastransparency");

        List<IntraDayIncomeSummary> intraDayIncomeSummaries = new List<IntraDayIncomeSummary>();

        dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
        var list = obj.body.incomes;

        foreach (var intraDayIncomeSummary in list)
        {
            intraDayIncomeSummaries.Add(new IntraDayIncomeSummary()
            {
                Date = DateTime.Parse(intraDayIncomeSummary.date.ToString()),
                Income = Decimal.Parse(intraDayIncomeSummary.income.ToString()),
                Period = Int32.Parse(intraDayIncomeSummary.period.ToString()),
                PeriodType = intraDayIncomeSummary.periodType.ToString()
            });
        }
        return intraDayIncomeSummaries;
    }
}

