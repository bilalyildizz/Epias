using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Dtos;
using Epias.Entities.Models;
using Epias.Models;
using Epias.Services.Utilities.Results.Interfaces;

namespace Epias.Services.Interfaces;
public interface IBulletinYearService
{
    Task<IResult> AddListAsync(List<BulletinYear> bulletinYears);
    Task<IDataResult<BulletinYearListDto>> GetAllByDateAsync();
}

