using System.Threading.Tasks;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Epias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntraDayAofController : ControllerBase
    {

        private readonly IIntraDayAofService _aofService;

        public IntraDayAofController(IIntraDayAofService aofService)
        {
            _aofService = aofService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _aofService.GetAllByDateAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
