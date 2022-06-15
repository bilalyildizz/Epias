using System.Threading.Tasks;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Epias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulletinDailyController : ControllerBase
    {
        private readonly IBulletinDailyService _bulletinDailyService;
        public BulletinDailyController(IBulletinDailyService bulletinDailyService)
        {
            _bulletinDailyService = bulletinDailyService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bulletinDailyService.GetAllByDateAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
