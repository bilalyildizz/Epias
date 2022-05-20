using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Concrete;

namespace Epias.Data.Abstract;

public interface ITradeHistoryRepository
{
    void Add(TradeHistory tradeHistory);
    void Update(TradeHistory tradeHistory);
    bool AnyAsync(Expression<Func<TradeHistory, bool>> predicate);
    IList<TradeHistory> GetAll();
}

