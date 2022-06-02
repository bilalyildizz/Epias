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

public class IntraDayIncomeApi : IIntraDayIncomeApi
{
    private readonly IHttpClientManager _httpClientManager;
    public IntraDayIncomeApi(IHttpClientManager httpClientManager)
    {
        _httpClientManager = httpClientManager;
    }
    public async Task<List<IntraDayIncome>> GetAll()
    {

        string endDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                         DateTime.Now.Day.ToString();
        string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                           DateTime.Now.Day.ToString();

        var response = await _httpClientManager
            .GetResponseWithRetries(
                $"market/intra-day-income?endDate={endDate}&startDate={startDate}", "epiastransparency");


        List<IntraDayIncome> intraDayIncomes = new List<IntraDayIncome>();

        dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
        var list = obj.body.incomes;

        foreach (var intraDayIncome in list)
        {
            intraDayIncomes.Add(new IntraDayIncome()
            {
                Date = DateTime.Parse(intraDayIncome.date.ToString()),
                Income = Decimal.Parse(intraDayIncome.income.ToString()),
            });
        }
        return intraDayIncomes;
    }
}


