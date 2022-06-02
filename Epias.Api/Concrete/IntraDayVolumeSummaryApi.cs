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

public class IntraDayVolumeSummaryApi : IIntraDayVolumeSummaryApi
{
    private readonly IHttpClientManager _httpClientManager;
    public IntraDayVolumeSummaryApi(IHttpClientManager httpClientManager)
    {
        _httpClientManager = httpClientManager;
    }
    public async Task<List<IntraDayVolumeSummary>> GetAll()
    {

        string endDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                         DateTime.Now.Day.ToString();
        string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                           DateTime.Now.Day.ToString();

        var response = await _httpClientManager
            .GetResponseWithRetries(
                $"market/intra-day-volume-summary?endDate={endDate}&DAILY&startDate={startDate}", "epiastransparency");


        List<IntraDayVolumeSummary> intraDayVolumeSummaries = new List<IntraDayVolumeSummary>();

        dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
        var list = obj.body.volumes;

        foreach (var intraDayVolumeSummary in list)
        {
            intraDayVolumeSummaries.Add(new IntraDayVolumeSummary()
            {
                Date = DateTime.Parse(intraDayVolumeSummary.date.ToString()),
                Volume = Double.Parse(intraDayVolumeSummary.volume.ToString()),
                Period = Int32.Parse(intraDayVolumeSummary.period.ToString()),
                PeriodType = intraDayVolumeSummary.periodType.ToString()
            });
        }

        return intraDayVolumeSummaries;
    }
}

