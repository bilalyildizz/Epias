using Epias.Models;
using Epias.Transparency.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epias.Transparency.Api.Concrete
{
    public class BulletinsWeeklyApi : IBulletinsWeeklyApi
    {
        private readonly IHttpClientManager _httpClientManager;

        public BulletinsWeeklyApi(IHttpClientManager httpClientManager)
        {
            _httpClientManager = httpClientManager;
        }

        public async Task<List<BulletinsWeekly>> GetAll()
        {
            string endDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                         DateTime.Now.Day.ToString();
            string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                               DateTime.Now.Day.ToString();

            var response = await _httpClientManager.GetResponseWithRetries(
                $"bulletin/weekly","epiastransparency");

            List<BulletinsWeekly> bulletinsWeeklys = new List<BulletinsWeekly>();
            if (response != null)
            {
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
                var list = obj.body.bulletins;
                foreach (var bulletinsWeekly in list)
                {
                    bulletinsWeeklys.Add(new BulletinsWeekly
                    {
                        Link = bulletinsWeekly.ToString(),
                        Title = bulletinsWeekly.ToString()
                    });
                }
            }
            return bulletinsWeeklys;
        }

     
    }
}
