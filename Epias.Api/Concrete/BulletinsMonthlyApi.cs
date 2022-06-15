using Epias.Models;
using Epias.Transparency.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epias.Transparency.Api.Concrete
{
    public class BulletinsMonthlyApi :IBulletinsMonthlyApi
    {
        private readonly IHttpClientManager _httpClientManager;

        public BulletinsMonthlyApi(IHttpClientManager httpClientManager)
        {
            _httpClientManager = httpClientManager;
        }

        public async Task<List<BulletinsMonthly>> GetAll()
        {
            string endDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                         DateTime.Now.Day.ToString();
            string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                               DateTime.Now.Day.ToString();

            var response = await _httpClientManager.GetResponseWithRetries(
                $"bulletin/monthly", "epiastransparency");

            List<BulletinsMonthly> bulletinsMonthlys = new List<BulletinsMonthly>();
            if (response != null)
            {
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
                var list = obj.body.bulletins;
                foreach (var bulletinsMonthly in list)
                {
                    bulletinsMonthlys.Add(new BulletinsMonthly
                    {
                        Link = bulletinsMonthly.link.ToString(),
                        Title = bulletinsMonthly.title.ToString()
                    });
                }
            }
            return bulletinsMonthlys;
        }

    }
}
