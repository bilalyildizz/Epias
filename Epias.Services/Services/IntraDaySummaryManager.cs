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
    public class IntraDaySummaryManager:IIntraDaySummaryService
    {
        private readonly IIntraDaySummaryRepository _intraDaySummaryRepository;
        public IntraDaySummaryManager(IIntraDaySummaryRepository intraDaySummaryRepository)
        {
            _intraDaySummaryRepository = intraDaySummaryRepository;
        }
        public async Task<IResult> AddListAsync(List<IntraDaySummary> intraDaySummaries)
        {
            foreach (var intraDaySummary in intraDaySummaries)
            {
                var result = await _intraDaySummaryRepository.AnyAsync(t=>t.IdApi==intraDaySummary.IdApi);
                if (!result)
                {
                    await _intraDaySummaryRepository.AddAsync(intraDaySummary);
                }
            }
            await _intraDaySummaryRepository.SaveChangesAsync();
            return new Result(ResultStatus.Success);
        }

        public async Task<IDataResult<IntraDaySummaryListDto>> GetAllByDateAsync()
        {
            var intraDaySummaries = await _intraDaySummaryRepository.GetAllByDateAsync();
            await _intraDaySummaryRepository.SaveChangesAsync();
            return new DataResult<IntraDaySummaryListDto>(ResultStatus.Success, new IntraDaySummaryListDto
            {
                IntraDaySummaries = intraDaySummaries
            });
        }
    }
}
