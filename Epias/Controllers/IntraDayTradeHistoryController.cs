using System.Threading.Tasks;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Epias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntraDayTradeHistoryController : ControllerBase
    {
        private readonly IIntraDayTradeHistoryService _tradeHistoryService;

        public IntraDayTradeHistoryController(IIntraDayTradeHistoryService tradeHistoryService)
        {
            _tradeHistoryService = tradeHistoryService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetTradeHistories()
        {
            var result = await _tradeHistoryService.GetAllByDateAsync();
            if (result.ResultStatus == ResultStatus.Success)
            { 
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
