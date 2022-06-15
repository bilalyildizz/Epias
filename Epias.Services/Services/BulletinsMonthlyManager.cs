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
    public class BulletinsMonthlyManager : IBulletinsMonthlyService
    {
        private readonly IBulletinsMonthlyRepository _bulletinMonthlyRepository;

        public BulletinsMonthlyManager(IBulletinsMonthlyRepository bulletinMonthlyRepository)
        {
            _bulletinMonthlyRepository = bulletinMonthlyRepository;
        }

        public async Task<IResult> AddListAsync(List<BulletinsMonthly> bulletinsMonthlys)
        {
            foreach (var bulletinsMonthly in bulletinsMonthlys)
            {
                var result = await _bulletinMonthlyRepository.Get(t => t.Title == bulletinsMonthly.Title);
                if (result != null)
                {
                    bulletinsMonthly.Id = result.Id;
                 
                    await _bulletinMonthlyRepository.UpdateAsync(bulletinsMonthly);
                }
                else
                {
                    await _bulletinMonthlyRepository.AddAsync(bulletinsMonthly);
                }
            }
            await _bulletinMonthlyRepository.SaveChangesAsync();
            return new Result(ResultStatus.Success);
        }

        public async Task<IDataResult<BulletinsMonthlyListDto>> GetAllByDateAsync()
        {
            var bulletinsMonthly = await _bulletinMonthlyRepository.GetAllByDateAsync();
            await _bulletinMonthlyRepository.SaveChangesAsync();
            return new DataResult<BulletinsMonthlyListDto>(ResultStatus.Success, new BulletinsMonthlyListDto
            {
                BulletinsMonthlys = bulletinsMonthly
            });
        }
    }
}
