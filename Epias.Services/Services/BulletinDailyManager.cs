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
    public class BulletinDailyManager:IBulletinDailyService
    {
        private readonly IBulletinDailyRepository _bulletinDailyRepository;

        public BulletinDailyManager(IBulletinDailyRepository bulletinDailyRepository)
        {
            _bulletinDailyRepository = bulletinDailyRepository;
        }
        public async Task<IResult> AddListAsync(List<BulletinDaily> bulletinDailies)
        {
            foreach (var bulletinDaily in bulletinDailies)
            {
                var result = await _bulletinDailyRepository.Get(t => t.Title == bulletinDaily.Title);
                if (result != null)
                {
                    bulletinDaily.Id = result.Id;
                    await _bulletinDailyRepository.UpdateAsync(bulletinDaily);
                }
                else
                {
                    await _bulletinDailyRepository.AddAsync(bulletinDaily);
                }
            }
            await _bulletinDailyRepository.SaveChangesAsync();
            return new Result(ResultStatus.Success);
        }

        public async Task<IDataResult<BulletinDailyListDto>> GetAllByDateAsync()
        {
            var date = DateTime.Today.AddDays(-1);
            var day = date.Day.ToString().Length == 1 ? "0" + date.Day.ToString() : date.Day.ToString();
            var month = date.Month.ToString().Length == 1 ? "0" + date.Month.ToString() : date.Month.ToString();
            var year = date.Year.ToString();
            string dateTitle = day + "." + month + "." + year;

            var bulletinDailies = await _bulletinDailyRepository.GetAllByDateAsync(b=>b.Title==dateTitle);
            await _bulletinDailyRepository.SaveChangesAsync();
            return new DataResult<BulletinDailyListDto>(ResultStatus.Success, new BulletinDailyListDto
            {
                BulletinDailies = bulletinDailies
            });
        }
    }
}
