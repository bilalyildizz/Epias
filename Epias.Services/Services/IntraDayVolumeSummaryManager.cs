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

public class IntraDayVolumeSummaryManager:IIntraDayVolumeSummaryService
{
    private readonly IIntraDayVolumeSummaryRepository _intraDayVolumeSummaryRepository;
    public IntraDayVolumeSummaryManager(IIntraDayVolumeSummaryRepository intraDayVolumeSummaryRepository)
    {
        _intraDayVolumeSummaryRepository = intraDayVolumeSummaryRepository;
    }
    public async Task<IResult> AddListAsync(List<IntraDayVolumeSummary> intraDayVolumeSummaries)
    {
        foreach (var intraDAyVolumeSummary in intraDayVolumeSummaries)
        {
            var result = await _intraDayVolumeSummaryRepository.Get(t => t.Date==intraDAyVolumeSummary.Date&& t.Period==intraDAyVolumeSummary.Period && t.PeriodType==intraDAyVolumeSummary.PeriodType);
            if (result!=null)
            {
                intraDAyVolumeSummary.Id = result.Id;
                await _intraDayVolumeSummaryRepository.UpdateAsync(intraDAyVolumeSummary);
            }
            else
            {
                await _intraDayVolumeSummaryRepository.AddAsync(intraDAyVolumeSummary);
            }
        }
        await _intraDayVolumeSummaryRepository.SaveChangesAsync();
        return new Result(ResultStatus.Success);
    }

    public async Task<IDataResult<IntraDayVolumeSummaryListDto>> GetAllByDateAsync()
    {
        var intraDayVolumeSummaries = await _intraDayVolumeSummaryRepository.GetAllByDateAsync();
        await _intraDayVolumeSummaryRepository.SaveChangesAsync();
        return new DataResult<IntraDayVolumeSummaryListDto>(ResultStatus.Success, new IntraDayVolumeSummaryListDto
        {
            IntraDayVolumeSummaries = intraDayVolumeSummaries
        });
    }
}
