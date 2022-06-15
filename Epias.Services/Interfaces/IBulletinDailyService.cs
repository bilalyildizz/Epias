using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Dtos;
using Epias.Entities.Models;
using Epias.Services.Utilities.Results.Interfaces;

namespace Epias.Services.Interfaces
{
    public interface IBulletinDailyService
    {
        Task<IResult> AddListAsync(List<BulletinDaily> bulletinDailies);
        Task<IDataResult<BulletinDailyListDto>> GetAllByDateAsync();
    }
}
