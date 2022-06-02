using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Models;

namespace Epias.Data.Interfaces;

public interface IIntraDayAofRepository : IRepository<IntraDayAof>
{
    public void Update(IntraDayAof intraDayAof);
}

