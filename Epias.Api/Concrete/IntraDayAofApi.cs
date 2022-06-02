using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Epias.Entities.Models;
using Epias.Transparency.Api.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonElement = System.Text.Json.JsonElement;

namespace Epias.Transparency.Api.Concrete;

public class IntraDayAofApi : IIntraDayAofApi
{
    private readonly IHttpClientManager _httpClientManager;
    public IntraDayAofApi(IHttpClientManager httpClientManager)
    {
        _httpClientManager = httpClientManager;
    }
    public async Task<List<IntraDayAof>> GetAll()
    {
        string endDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                         DateTime.Now.Day.ToString();
        string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                           DateTime.Now.Day.ToString();

        var response = await _httpClientManager.GetResponseWithRetries(
            $"market/intra-day-aof?endDate={endDate}&startDate={startDate}", "epiastransparency");

        List<IntraDayAof> intraDayAofs = new List<IntraDayAof>();
        if (response != null)
        {
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
            var list = obj.body.idmAofList;
            foreach (var intraDayAof in list)
            {
                intraDayAofs.Add(new IntraDayAof
                {
                    Date = DateTime.Parse(intraDayAof.date.ToString()),
                    Price = Decimal.Parse(intraDayAof.price.ToString())
                });
            }
        }
        return intraDayAofs;
    }
}

