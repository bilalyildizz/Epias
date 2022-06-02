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

namespace Epias.Services.Services;

public class IntraDayIncomeSummaryManager : IIntraDayIncomeSummaryService
{
    private readonly IIntraDayIncomeSummaryRepository _intraDayIncomeSummaryRepository;
    public IntraDayIncomeSummaryManager(IIntraDayIncomeSummaryRepository intraDayIncomeSummaryRepository)
    {
        _intraDayIncomeSummaryRepository = intraDayIncomeSummaryRepository;
    }

    public async Task<IResult> AddListAsync(List<IntraDayIncomeSummary> intraDayIncomeSummaries)
    {

        foreach (var intraDayIncomeSummary in intraDayIncomeSummaries)
        {
            var result = await _intraDayIncomeSummaryRepository.AnyAsync(t => t.Date == intraDayIncomeSummary.Date && t.Period == intraDayIncomeSummary.Period && t.Income == intraDayIncomeSummary.Income && t.PeriodType == intraDayIncomeSummary.PeriodType);
            if (!result)
            {
                await _intraDayIncomeSummaryRepository.AddAsync(intraDayIncomeSummary);
            }
        }
        await _intraDayIncomeSummaryRepository.SaveChangesAsync();
        return new Result(ResultStatus.Success);
    }

    public async Task<IDataResult<IntraDayIncomeSummaryListDto>> GetAllByDateAsync()
    {
        var intraDayIncomeSummaries = await _intraDayIncomeSummaryRepository.GetAllByDateAsync();
        await _intraDayIncomeSummaryRepository.SaveChangesAsync();
        return new DataResult<IntraDayIncomeSummaryListDto>(ResultStatus.Success, new IntraDayIncomeSummaryListDto
        {
            IntraDayIncomeSummaries = intraDayIncomeSummaries
        });
    }
}

