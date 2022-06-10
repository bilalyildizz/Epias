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

public class IntraDayIncomeManager : IIntraDayIncomeService
{
    private readonly IIntraDayIncomeRepository _intraDayIncomeRepository;
    public IntraDayIncomeManager(IIntraDayIncomeRepository intraDayIncomeRepository)
    {
        _intraDayIncomeRepository = intraDayIncomeRepository;
    }

    public async Task<IResult> AddListAsync(List<IntraDayIncome> intraDayIncomes)
    {
        foreach (var intraDayIncome in intraDayIncomes)
        {
            var result = await _intraDayIncomeRepository.Get(t => t.Date == intraDayIncome.Date);
            if (result!=null)
            {
                intraDayIncome.Id = result.Id;
                await _intraDayIncomeRepository.UpdateAsync(intraDayIncome);
            }
            else
            {
                await _intraDayIncomeRepository.AddAsync(intraDayIncome);
            }
        }
        await _intraDayIncomeRepository.SaveChangesAsync();
        return new Result(ResultStatus.Success);
    }

    public async Task<IDataResult<IntraDayIncomeListDto>> GetAllByDateAsync()
    {
        var intraDayIncomes = await _intraDayIncomeRepository.GetAllByDateAsync(t => t.Date.Date == DateTime.Today);
        await _intraDayIncomeRepository.SaveChangesAsync();
        return new DataResult<IntraDayIncomeListDto>(ResultStatus.Success, new IntraDayIncomeListDto
        {
            IntraDayIncomes = intraDayIncomes
        });
    }
}

