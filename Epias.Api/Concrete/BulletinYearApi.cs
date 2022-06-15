using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Models;
using Epias.Transparency.Api.Interfaces;

namespace Epias.Transparency.Api.Concrete
{
    public class BulletinYearApi:IBulletinYearApi
    {
        private readonly IHttpClientManager _httpClientManager;
        public BulletinYearApi(IHttpClientManager httpClientManager)
        {
            _httpClientManager = httpClientManager;
        }
        public async Task<List<BulletinYear>> GetAll()
        {

            var response = await _httpClientManager.GetResponseWithRetries(
                $"bulletin/year", "epiastransparency");

            List<BulletinYear> bulletinYears = new List<BulletinYear>();
            if (response != null)
            {
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
                var list = obj.body.bulletins;
                foreach (var bulletinYear in list)
                {
                    bulletinYears.Add(new BulletinYear()
                    {
                        Link = bulletinYear.link.ToString(),
                        Title = bulletinYear.title.ToString()
                    });
                }
            }
            return bulletinYears;
        }
    }
}
