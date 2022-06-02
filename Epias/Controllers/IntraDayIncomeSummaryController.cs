using System.Threading.Tasks;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Epias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntraDayIncomeSummaryController : ControllerBase
    {

        private readonly IIntraDayIncomeSummaryService _incomeSummaryService;

        public IntraDayIncomeSummaryController(IIntraDayIncomeSummaryService incomeSummaryService)
        {
            _incomeSummaryService = incomeSummaryService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _incomeSummaryService.GetAllByDateAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
