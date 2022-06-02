using System.Threading.Tasks;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Epias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntraDaySummaryController : ControllerBase
    {
        private readonly IIntraDaySummaryService _summaryService;

        public IntraDaySummaryController(IIntraDaySummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _summaryService.GetAllByDateAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
