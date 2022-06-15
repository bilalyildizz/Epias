using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Data.Concrete.EntityFramework.Contexts;
using Epias.Data.Interfaces;
using Epias.Entities.Models;

namespace Epias.Data.Concrete.EntityFramework.Repostories;
public class EfBulletinYearRepository:EfRepositoryBase<BulletinYear>,IBulletinYearRepository
{
    public EfBulletinYearRepository(EpiasDbContext context) : base(context)
    {
    }
}

