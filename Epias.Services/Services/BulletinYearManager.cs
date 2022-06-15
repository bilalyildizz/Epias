using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Data.Interfaces;
using Epias.Entities.Dtos;
using Epias.Entities.Models;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Epias.Services.Utilities.Results.Concrete;
using Epias.Services.Utilities.Results.Interfaces;

namespace Epias.Services.Services
{
    public class BulletinYearManager:IBulletinYearService
    {
        private readonly IBulletinYearRepository _bulletinYearRepository;

        public BulletinYearManager(IBulletinYearRepository bulletinYearRepository)
        {
            _bulletinYearRepository = bulletinYearRepository;
        }

        public async Task<IResult> AddListAsync(List<BulletinYear> bulletinYears)
        {

            foreach (var bulletinWeekly in bulletinYears)
            {
                var result = await _bulletinYearRepository.Get(t => t.Title == bulletinWeekly.Title);
                if (result != null)
                {
                    bulletinWeekly.Id = result.Id;
                    await _bulletinYearRepository.UpdateAsync(bulletinWeekly);
                }
                else
                {
                    await _bulletinYearRepository.AddAsync(bulletinWeekly);
                }
            }
            await _bulletinYearRepository.SaveChangesAsync();
            return new Result(ResultStatus.Success);
        }

        public async Task<IDataResult<BulletinYearListDto>> GetAllByDateAsync()
        {



            var bulletinYears = await _bulletinYearRepository.GetAllByDateAsync();
            await _bulletinYearRepository.SaveChangesAsync();
            return new DataResult<BulletinYearListDto>(ResultStatus.Success, new BulletinYearListDto
            {
                BulletinYears = bulletinYears
            });
        }
    }
}
