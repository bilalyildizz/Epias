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

public class IntraDayAofManager : IIntraDayAofService
{
    private readonly IIntraDayAofRepository _intraDayAofRepository;
    public IntraDayAofManager(IIntraDayAofRepository intraDayAofRepository)
    {
        _intraDayAofRepository = intraDayAofRepository;
    }

    public async Task<IResult> AddListAsync(List<IntraDayAof> intraDayAofs)
    {

        foreach (var intraDayAof in intraDayAofs)
        {
            var result = await _intraDayAofRepository.AnyAsync(t => t.Date == intraDayAof.Date && t.Price==intraDayAof.Price);
            if (!result)
            {
                await _intraDayAofRepository.AddAsync(intraDayAof);
            }
        }
        await _intraDayAofRepository.SaveChangesAsync();
        return new Result(ResultStatus.Success);
    }

    public async Task<IDataResult<IntraDayAofListDto>> GetAllByDateAsync()
    {
        var intraDayAofs = await _intraDayAofRepository.GetAllByDateAsync(t => t.Date.Date == DateTime.Today);
        await _intraDayAofRepository.SaveChangesAsync();
        return new DataResult<IntraDayAofListDto>(ResultStatus.Success, new IntraDayAofListDto
        {
            IntraDayAofs = intraDayAofs
        });
    }
}

