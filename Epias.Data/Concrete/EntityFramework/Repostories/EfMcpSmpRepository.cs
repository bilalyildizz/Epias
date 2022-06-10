using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Data.Concrete.EntityFramework.Contexts;
using Epias.Data.Interfaces;
using Epias.Models;

namespace Epias.Data.Concrete.EntityFramework.Repostories;

public class EfMcpSmpRepository:EfRepositoryBase<McpSmp>,IMcpSmpRepository
{
    public EfMcpSmpRepository(EpiasDbContext context) : base(context)
    {
    }
}

