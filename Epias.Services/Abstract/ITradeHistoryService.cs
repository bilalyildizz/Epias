using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Concrete;

namespace Epias.Services.Abstract;

public interface ITradeHistoryService
{
    void AddList(List<TradeHistory> tradeHistories);
    IList<TradeHistory> GetAll();
}

