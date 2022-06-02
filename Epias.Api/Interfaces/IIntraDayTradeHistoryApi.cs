using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Models;

namespace Epias.Api;

public interface IIntraDayTradeHistoryApi
{
    Task<List<IntraDayTradeHistory>> GetAll();
}

