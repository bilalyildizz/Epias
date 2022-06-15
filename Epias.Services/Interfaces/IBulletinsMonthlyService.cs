using Epias.Entities.Dtos;
using Epias.Models;
using Epias.Services.Utilities.Results.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epias.Services.Interfaces
{
    public interface IBulletinsMonthlyService
    {
        Task<IResult> AddListAsync(List<BulletinsMonthly> bulletinsMonthlys);
        Task<IDataResult<BulletinsMonthlyListDto>> GetAllByDateAsync();
    }
}
