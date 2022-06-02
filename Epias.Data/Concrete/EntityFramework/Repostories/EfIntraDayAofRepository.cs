using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Data.Concrete.EntityFramework.Contexts;
using Epias.Data.Interfaces;
using Epias.Entities.Models;

namespace Epias.Data.Concrete.EntityFramework.Repostories;

public class EfIntraDayAofRepository : EfRepositoryBase<IntraDayAof>, IIntraDayAofRepository
{
    public EfIntraDayAofRepository(EpiasDbContext context) : base(context)
    {
    }

    public void Update(IntraDayAof intraDayAof)
    {
        _context.Set<IntraDayAof>().Update(intraDayAof);
    }
}

