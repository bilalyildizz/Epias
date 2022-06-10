using Epias.Data.Interfaces;
using Epias.Entities.Dtos;
using Epias.Models;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Epias.Services.Utilities.Results.Concrete;
using Epias.Services.Utilities.Results.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epias.Services.Services
{

    public class BulletinsWeeklyManager : IBulletinsWeeklyService
    {
        private readonly IBulletinWeeklyRepository _bulletinWeeklyRepository;

        public BulletinsWeeklyManager(IBulletinWeeklyRepository bulletinWeeklyRepository)
        {
            _bulletinWeeklyRepository = bulletinWeeklyRepository;
        }

        public async Task<IResult> AddListAsync(List<BulletinsWeekly> bulletinsWeeklys)
        {
            foreach (var bulletinWeekly in bulletinsWeeklys)
            {
                var result = await _bulletinWeeklyRepository.Get(t => t.Title == bulletinWeekly.Title);
                if (result != null)
                {
                    bulletinWeekly.Id = result.Id;
                    await _bulletinWeeklyRepository.UpdateAsync(bulletinWeekly);
                }
                else
                {
                    await _bulletinWeeklyRepository.AddAsync(bulletinWeekly);
                }
            }
            await _bulletinWeeklyRepository.SaveChangesAsync();
            return new Result(ResultStatus.Success);
        }

        public async Task<IDataResult<BulletinsWeeklyListDto>> GetAllByDateAsync()
        {
            var bulletinWeekly = await _bulletinWeeklyRepository.GetAllByDateAsync();
            await _bulletinWeeklyRepository.SaveChangesAsync();
            return new DataResult<BulletinsWeeklyListDto>(ResultStatus.Success, new BulletinsWeeklyListDto
            {
                BulletinsWeeklys = bulletinWeekly
            });
        }
    }
}
