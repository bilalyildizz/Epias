using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Epias.Api;
using Epias.Entities.Models;

namespace Epias.Transparency.Api.Concrete;

public class IntraDayTradeHistoryApi : IIntraDayTradeHistoryApi
{
    private readonly IHttpClientManager _httpClientManager;
    public IntraDayTradeHistoryApi(IHttpClientManager httpClientManager)
    {
        _httpClientManager = httpClientManager;
    }
    public async Task<List<IntraDayTradeHistory>> GetAll()
    {
        string endDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                         DateTime.Now.Day.ToString();
        string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                           DateTime.Now.Day.ToString();

        var response = await _httpClientManager
            .GetResponseWithRetries(
                $"market/intra-day-trade-history?endDate={endDate}&startDate={startDate}", "epiastransparency");

        List<IntraDayTradeHistory> intraDayTradeHistories = new List<IntraDayTradeHistory>();

        dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
        var list = obj.body.intraDayTradeHistoryList;

        foreach (var intraDayTradeHistory in list)
        {
            intraDayTradeHistories.Add(new IntraDayTradeHistory()
            {
                IdApi = long.Parse(intraDayTradeHistory.id.ToString()),
                Date = DateTime.Parse(intraDayTradeHistory.date.ToString()),
                Contract = intraDayTradeHistory.conract.ToString(),
                Price = Decimal.Parse(intraDayTradeHistory.price.ToString()),
                Quantity = Convert.ToInt32(intraDayTradeHistory.quantity.ToString())
            });
        }

        return intraDayTradeHistories;

    }
}


