using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Models;
using Epias.Models;
using Epias.Transparency.Api.Interfaces;

namespace Epias.Transparency.Api.Concrete;

public class McpSmpApi : IMcpSmpApi
{

    private readonly IHttpClientManager _httpClientManager;
    public McpSmpApi(IHttpClientManager httpClientManager)
    {
        _httpClientManager = httpClientManager;
    }
    public async Task<List<McpSmp>> GetAll()
    {
        string endDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                         DateTime.Now.Day.ToString();
        string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                           DateTime.Now.Day.ToString();

        var response = await _httpClientManager.GetResponseWithRetries(
            $"market/mcp-smp?endDate={endDate}&startDate={startDate}", "epiastransparency");

        List<McpSmp> mcpSmps = new List<McpSmp>();
        if (response != null)
        {
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
            var list = obj.body.mcpSmps;

            foreach (var mcpSmp in list)
            {
                if (mcpSmp.smp != null)
                {
                    mcpSmps.Add(new McpSmp()
                    {
                        Date = DateTime.Parse(mcpSmp.date.ToString()),
                        Mcp = Convert.ToDecimal(mcpSmp.mcp),
                        McpState = mcpSmp.mcpState.ToString(),
                        Smp = Convert.ToDecimal(mcpSmp.smp),
                        SmpDirection = mcpSmp.smpDirection.ToString()
                    });
                }
                else
                {
                    mcpSmps.Add(new McpSmp()
                    {
                        Date = DateTime.Parse(mcpSmp.date.ToString()),
                        Mcp = Convert.ToDecimal(mcpSmp.mcp),
                        McpState = mcpSmp.mcpState.ToString(),
                    });
                }

            }
        }
        return mcpSmps;
    }

}

