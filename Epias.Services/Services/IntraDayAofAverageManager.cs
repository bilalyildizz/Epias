using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Data.Abstract;
using Epias.Data.Interfaces;
using Epias.Entities.Dtos;
using Epias.Entities.Models;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Epias.Services.Utilities.Results.Concrete;
using Epias.Services.Utilities.Results.Interfaces;

namespace Epias.Services.Services;

public class IntraDayAofAverageManager : IIntraDayAofAverageService
{
    private readonly IIntraDayAofAverageRepository _intraDayAofAverageRepository;
    public IntraDayAofAverageManager(IIntraDayAofAverageRepository intraDayAofAverageRepository)
    {
        _intraDayAofAverageRepository = intraDayAofAverageRepository;
    }

    public async Task<IResult> AddListAsync(List<IntraDayAofAverage> intraDayAofAverages)
    {
        foreach (var intraDayAofAverage in intraDayAofAverages)
        {
            var result = await _intraDayAofAverageRepository.AnyAsync(t => t.Date == intraDayAofAverage.Date && t.Period == intraDayAofAverage.Period && t.Aof == intraDayAofAverage.Aof && t.PeriodType== intraDayAofAverage.PeriodType);
            if (!result)
            {
                await _intraDayAofAverageRepository.AddAsync(intraDayAofAverage);
            }
        }
        await _intraDayAofAverageRepository.SaveChangesAsync();
        return new Result(ResultStatus.Success);
    }

    public async Task<IDataResult<IntraDayAofAverageListDto>> GetAllByDateAsync()
    {
        var intraDayAofAverages = await _intraDayAofAverageRepository.GetAllByDateAsync();
        await _intraDayAofAverageRepository.SaveChangesAsync();
        return new DataResult<IntraDayAofAverageListDto>(ResultStatus.Success, new IntraDayAofAverageListDto
        {
            IntraDayAofAverages = intraDayAofAverages
        });
    }
}

