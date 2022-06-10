using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Data.Abstract;
using Epias.Entities.Dtos;
using Epias.Entities.Models;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Epias.Services.Utilities.Results.Concrete;
using Epias.Services.Utilities.Results.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Epias.Services.Services;

public class IntraDayTradeHistoryManager : IIntraDayTradeHistoryService
{
    private readonly IIntraDayTradeHistoryRepository _tradeHistoryRepository;
    public IntraDayTradeHistoryManager(IIntraDayTradeHistoryRepository tradeHistoryRepository)
    {
        _tradeHistoryRepository = tradeHistoryRepository;
    }

    public async Task<IResult> AddListAsync(List<IntraDayTradeHistory> tradeHistories)
    {
        foreach (var tradeHistory in tradeHistories)
        {
            var result = await _tradeHistoryRepository.Get(t => t.IdApi == tradeHistory.IdApi);
            if (result!=null)
            {
                tradeHistory.Id = result.Id;
                await _tradeHistoryRepository.UpdateAsync(tradeHistory);
            }
            else
            {
                await _tradeHistoryRepository.AddAsync(tradeHistory);
            }
        }
        await _tradeHistoryRepository.SaveChangesAsync();
        return new Result(ResultStatus.Success);
    }

    public async Task<IDataResult<IntraDayTradeHistoryListDto>> GetAllByDateAsync()
    {
        var tradeHistories = await _tradeHistoryRepository.GetAllByDateAsync(t => t.Date.Date == DateTime.Today);
        await _tradeHistoryRepository.SaveChangesAsync();
        return new DataResult<IntraDayTradeHistoryListDto>(ResultStatus.Success, new IntraDayTradeHistoryListDto
        {
            IntraDayTradeHistories = tradeHistories
        });
    }
}

