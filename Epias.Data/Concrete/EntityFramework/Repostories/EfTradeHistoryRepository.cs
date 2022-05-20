using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Epias.Data.Abstract;
using Epias.Data.Concrete.EntityFramework.Contexts;
using Epias.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Epias.Data.Concrete.EntityFramework.Repostories;

public class EfTradeHistoryRepository : ITradeHistoryRepository
{
    protected readonly EpiasContext _context;
    public EfTradeHistoryRepository(EpiasContext context)
    {
        _context = context;
    }

    public void Add(TradeHistory tradeHistory)
    {
        _context.Set<TradeHistory>().Add(tradeHistory);
        _context.SaveChanges();
    }

    public void Update(TradeHistory tradeHistory)
    {
        _context.Set<TradeHistory>().Update(tradeHistory);
        _context.SaveChanges();
    }

    public bool AnyAsync(Expression<Func<TradeHistory, bool>> predicate)
    {
        return _context.Set<TradeHistory>().Any(predicate);
    }

    public IList<TradeHistory> GetAll()
    {
        return _context.Set<TradeHistory>().ToList();
    }
}

