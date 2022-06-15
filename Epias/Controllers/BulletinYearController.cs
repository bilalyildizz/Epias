using System.Threading.Tasks;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Epias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulletinYearController : ControllerBase
    {

        private readonly IBulletinYearService _bulletinYearService;
        public BulletinYearController(IBulletinYearService bulletinYearService)
        {
            _bulletinYearService = bulletinYearService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bulletinYearService.GetAllByDateAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
