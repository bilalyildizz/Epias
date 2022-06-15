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
                $"market/intra-day-summary?endDate={endDate}&startDate={startDate}", "epiastransparency");




        List<IntraDaySummary> intraDaySummaries = new List<IntraDaySummary>();

        dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
        var list = obj.body.intraDaySummaryList;

        foreach (var intraDaySummary in list)
        {
            var summary = new IntraDaySummary();
            intraDaySummaries.Add(new IntraDaySummary()
            {
                Date = DateTime.TryParse(intraDaySummary.date.ToString(), out DateTime _) ? DateTime.Parse(intraDaySummary.date.ToString()) : null,
                IdApi = long.TryParse(intraDaySummary.id.ToString(), out long _) ? long.Parse(intraDaySummary.id.ToString()) : 0,
                Contract = intraDaySummary.contract != null ? intraDaySummary.contract.ToString() : null,
                MaxAskPrice = Decimal.TryParse(intraDaySummary.maxAskPrice.ToString(), out decimal _) ? Decimal.Parse(intraDaySummary.maxAskPrice.ToString()) : 0,
                MaxBidPrice = Decimal.TryParse(intraDaySummary.maxBidPrice.ToString(), out decimal _) ? Decimal.Parse(intraDaySummary.maxBidPrice.ToString()) : 0,
                MaxMatchPrice = Decimal.TryParse(intraDaySummary.maxMatchPrice.ToString(), out decimal _) ? Decimal.Parse(intraDaySummary.maxMatchPrice.ToString()) : 0,
                MinAskPrice = Decimal.TryParse(intraDaySummary.minAskPrice.ToString(), out decimal _) ? Decimal.Parse(intraDaySummary.minAskPrice.ToString()) : 0,
                MinBidPrice = Decimal.TryParse(intraDaySummary.minBidPrice.ToString(), out decimal _) ? Decimal.Parse(intraDaySummary.minBidPrice.ToString()) : 0,
                MinMatchPrice = Decimal.TryParse(intraDaySummary.minMatchPrice.ToString(), out decimal _) ? Decimal.Parse(intraDaySummary.minMatchPrice.ToString()) : 0,
                QuantityOfAsk = Double.TryParse(intraDaySummary.quantityOfAsk.ToString(), out double _) ? Double.Parse(intraDaySummary.quantityOfAsk.ToString()) : 0,
                QuantityOfBid = Double.TryParse(intraDaySummary.quantityOfBid.ToString(), out double _) ? Double.Parse(intraDaySummary.quantityOfBid.ToString()) : 0,
                TradingVolume = Double.TryParse(intraDaySummary.tradingVolume.ToString(), out double _) ? Double.Parse(intraDaySummary.tradingVolume.ToString()) : 0,
                Volume = Double.TryParse(intraDaySummary.volume.ToString(), out double _) ? Double.Parse(intraDaySummary.volume.ToString()) : 0
            });
        }

        return intraDaySummaries;


    }

}

