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

public class IntraDayQuantityManager : IIntraDayQuantityService
{
    private readonly IIntraDayQuantityRepository _intraDayQuantityRepository;

    public IntraDayQuantityManager(IIntraDayQuantityRepository intraDayQuantityRepository)
    {
        _intraDayQuantityRepository = intraDayQuantityRepository;
    }

    public async Task<IResult> AddListAsync(List<IntraDayQuantity> intraDayQuantities)
    {
        foreach (var intraDayQuantity in intraDayQuantities)
        {
            var result = await _intraDayQuantityRepository.AnyAsync(t => t.HourlyPurchaseQuantity == intraDayQuantity.HourlyPurchaseQuantity && t.EffectiveDate ==intraDayQuantity.EffectiveDate && t.HourlySaleQuantity==intraDayQuantity.HourlySaleQuantity);
            if (!result)
            {
                await _intraDayQuantityRepository.AddAsync(intraDayQuantity);
            }
        }
        await _intraDayQuantityRepository.SaveChangesAsync();
        return new Result(ResultStatus.Success);
    }

    public async Task<IDataResult<IntraDayQuantityListDto>> GetAllByDateAsync()
    {
        var intraDayIncomeSummaries = await _intraDayQuantityRepository.GetAllByDateAsync();
        await _intraDayQuantityRepository.SaveChangesAsync();
        return new DataResult<IntraDayQuantityListDto>(ResultStatus.Success, new IntraDayQuantityListDto
        {
            IntraDayQuantities = intraDayIncomeSummaries
        });
    }
}

