using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Data.Abstract;
using Epias.Entities.Concrete;
using Epias.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Epias.Services.Concrete;

public class TradeHistoryManager : ITradeHistoryService
{
    private readonly ITradeHistoryRepository _tradeHistoryRepository;
    public TradeHistoryManager(ITradeHistoryRepository tradeHistoryRepository)
    {
        _tradeHistoryRepository = tradeHistoryRepository;
    }
    public void AddList(List<TradeHistory> tradeHistories)
    {
        foreach (var tradeHistory in tradeHistories)
        {
            var result = _tradeHistoryRepository.AnyAsync(t => t.Id == tradeHistory.Id);
            if (result)
            {
                _tradeHistoryRepository.Update(tradeHistory);
            }
            else
            {
                _tradeHistoryRepository.Add(tradeHistory);
            }
        }
    }

    public IList<TradeHistory> GetAll()
    {
        var tradeHistories = _tradeHistoryRepository.GetAll();
        return tradeHistories;
    }
}

