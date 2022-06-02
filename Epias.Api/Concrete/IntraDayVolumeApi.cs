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

public class IntraDayVolumeApi : IIntraDayVolumeApi
{
    private readonly IHttpClientManager _httpClientManager;
    public IntraDayVolumeApi(IHttpClientManager httpClientManager)
    {
        _httpClientManager = httpClientManager;
    }
    public async Task<List<IntraDayVolume>> GetAll()
    {

        string endDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                         DateTime.Now.Day.ToString();
        string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                           DateTime.Now.Day.ToString();

        var response = await _httpClientManager
            .GetResponseWithRetries(
                $"https://seffaflik.epias.com.tr/transparency/service/market/intra-day-volume?endDate={endDate}&startDate={startDate}", "epiastransparency");


        List<IntraDayVolume> intraDayVolumes = new List<IntraDayVolume>();

        dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
        var list = obj.body.matchDetails;

        foreach (var intraDayVolume in list)
        {
            intraDayVolumes.Add(new IntraDayVolume()
            {
                BlockMatchQuantity = Double.Parse(intraDayVolume.blockMatchQuantity.ToString()),
                HourlyMatchQuantity = Double.Parse(intraDayVolume.hourlyMatchQuantity.ToString()),
                Date = DateTime.Parse(intraDayVolume.date.ToString()),
            });
        }

        return intraDayVolumes;

    }

}

