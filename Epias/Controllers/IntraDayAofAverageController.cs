using System.Threading.Tasks;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Epias.Transparency.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Epias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntraDayAofAverageController : ControllerBase
    {

        private readonly IIntraDayAofAverageService _aofAverageService;

        public IntraDayAofAverageController(IIntraDayAofAverageService aofAverageService)
        {
            _aofAverageService = aofAverageService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _aofAverageService.GetAllByDateAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
