using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Models;
using Epias.Transparency.Api.Interfaces;

namespace Epias.Transparency.Api.Concrete;

public class BulletinDailyApi : IBulletinDailyApi
{
    private readonly IHttpClientManager _httpClientManager;

    public BulletinDailyApi(IHttpClientManager httpClientManager)
    {
        _httpClientManager = httpClientManager;
    }

    public async Task<List<BulletinDaily>> GetAll()
    {
        var date = DateTime.Today.AddDays(-1);

        string dateParameter = date.Year.ToString() + "-" + date.Month.ToString() + "-" +
                           date.Day.ToString();

        var response = await _httpClientManager.GetResponseWithRetries(
            $"bulletin/daily?date={dateParameter}", "epiastransparency");

        List<BulletinDaily> bulletinDailies = new List<BulletinDaily>();
        if (response != null)
        {
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
            var list = obj.body.bulletins;
            foreach (var bulletinDaily in list)
            {
                bulletinDailies.Add(new BulletinDaily
                {
                    Link = bulletinDaily.link.ToString(),
                    Title = bulletinDaily.title.ToString()
                });
            }
        }
        return bulletinDailies;
    }
}

