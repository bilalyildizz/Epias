using Epias.Data.Concrete.EntityFramework.Contexts;
using Epias.Data.Interfaces;
using Epias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epias.Data.Concrete.EntityFramework.Repostories
{
    public class EfBulletinsMonthlyRepository : EfRepositoryBase<BulletinsMonthly>, IBulletinsMonthlyRepository
    
    {
        public EfBulletinsMonthlyRepository(EpiasDbContext context) : base(context)
        {
        }
    }
}
