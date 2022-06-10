using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Epias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulletinsWeeklyController : ControllerBase
    {
        private readonly IBulletinsWeeklyService _bulletinsWeeklyService;
        public BulletinsWeeklyController(IBulletinsWeeklyService bulletinsWeeklyService)
        {
            _bulletinsWeeklyService = bulletinsWeeklyService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bulletinsWeeklyService.GetAllByDateAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
