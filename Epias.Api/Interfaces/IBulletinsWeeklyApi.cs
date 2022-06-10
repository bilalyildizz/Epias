using Epias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epias.Transparency.Api.Interfaces
{
    public interface IBulletinsWeeklyApi
    {
        Task<List<BulletinsWeekly>> GetAll();
    }
}
