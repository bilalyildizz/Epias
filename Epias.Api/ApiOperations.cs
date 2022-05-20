using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Epias.Entities.Concrete;

namespace Epias.Api;

public class ApiOperations : IApiOperations
{
    public List<TradeHistory> GetTradeHistories()
    {
        using (var client = new HttpClient())
        {
            string endDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                             DateTime.Now.Day.ToString();
            string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                               DateTime.Now.Day.ToString();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            HttpResponseMessage response = client
                .GetAsync(
                    $"https://seffaflik.epias.com.tr/transparency/service/market/intra-day-trade-history?endDate={endDate}&startDate={startDate}")
                .Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                XDocument xdoc = XDocument.Parse(response.Content.ReadAsStringAsync().Result);

                var tradeHistories = xdoc.Elements("intraDayTradeHistoryResponse").Elements("body")
                    .Elements("intraDayTradeHistoryList")
                    .Select(p => new TradeHistory
                    {
                        Id = p.Element("id").Value,
                        Date = DateTime.Parse(p.Element("date").Value),
                        Conract = p.Element("conract").Value,
                        Price = Convert.ToDouble(p.Element("price").Value),
                        Quantity = Convert.ToInt32(p.Element("quantity").Value)
                    })
                    .ToList();
                return tradeHistories;
            }
            return new List<TradeHistory>();
        }
    }
}

