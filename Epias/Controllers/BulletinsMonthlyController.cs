using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Epias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulletinsMonthlyController : ControllerBase
    {
        private readonly IBulletinsMonthlyService _bulletinsMonthlyService;
        public BulletinsMonthlyController(IBulletinsMonthlyService bulletinsMonthlyService)
        {
            _bulletinsMonthlyService = bulletinsMonthlyService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bulletinsMonthlyService.GetAllByDateAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
