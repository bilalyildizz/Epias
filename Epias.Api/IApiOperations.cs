using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Concrete;

namespace Epias.Api;

public interface IApiOperations
{
    List<TradeHistory> GetTradeHistories();
}

