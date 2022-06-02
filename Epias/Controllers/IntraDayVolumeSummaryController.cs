using System.Threading.Tasks;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Epias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntraDayVolumeSummaryController : ControllerBase
    {
        private readonly IIntraDayVolumeSummaryService _volumeSummaryService;

        public IntraDayVolumeSummaryController(IIntraDayVolumeSummaryService volumeSummaryService)
        {
            _volumeSummaryService = volumeSummaryService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _volumeSummaryService.GetAllByDateAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
