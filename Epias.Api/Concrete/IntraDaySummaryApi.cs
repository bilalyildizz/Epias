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

public class IntraDaySummaryApi : IIntraDaySummaryApi
{
    private readonly IHttpClientManager _httpClientManager;
    public IntraDaySummaryApi(IHttpClientManager httpClientManager)
    {
        _httpClientManager = httpClientManager;
    }

    public async Task<List<IntraDaySummary>> GetAll()
    {
        string endDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                         DateTime.Now.Day.ToString();
        string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                           DateTime.Now.Day.ToString();

        var response = await _httpClientManager.GetResponseWithRetries(
                $"market/intra-day-summary?endDate={endDate}&HOURLY&startDate={startDate}", "epiastransparency");




        List<IntraDaySummary> intraDaySummaries = new List<IntraDaySummary>();

        dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
        var list = obj.body.intraDaySummaryList;

        foreach (var intraDaySummary in list)
        {
            intraDaySummaries.Add(new IntraDaySummary()
            {
                Date = DateTime.Parse(intraDaySummary.date.ToString()),
                IdApi = long.Parse(intraDaySummary.id.ToString()),
                Contract = intraDaySummary.contract.ToString(),
                MaxAskPrice = Decimal.Parse(intraDaySummary.maxAskPrice.ToString()),
                MaxBidPrice = Decimal.Parse(intraDaySummary.maxBidPrice.ToString()),
                MaxMatchPrice = Decimal.Parse(intraDaySummary.maxMatchPrice.ToString()),
                MinAskPrice = Decimal.Parse(intraDaySummary.minAskPrice.ToString()),
                MinBidPrice = Decimal.Parse(intraDaySummary.minBidPrice.ToString()),
                MinMatchPrice = Decimal.Parse(intraDaySummary.minMatchPrice.ToString()),
                QuantityOfAsk = Double.Parse(intraDaySummary.quantityOfAsk.ToString()),
                QuantityOfBid = Double.Parse(intraDaySummary.quantityOfBid.ToString()),
                TradingVolume = Double.Parse(intraDaySummary.tradingVolume.ToString()),
                Volume = Double.Parse(intraDaySummary.volume.ToString())
            });
        }

        return intraDaySummaries;


    }

}

