using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Epias.Data.Abstract;
using Epias.Data.Concrete.EntityFramework.Contexts;
using Epias.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Epias.Data.Concrete.EntityFramework.Repostories;

public class EfIntraDayTradeHistoryRepository : EfRepositoryBase<IntraDayTradeHistory>, IIntraDayTradeHistoryRepository
{
    public EfIntraDayTradeHistoryRepository(EpiasDbContext context) : base(context)
    {
    }
}

