using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Models;

namespace Epias.Entities.Dtos;

public class IntraDayIncomeListDto
{
    public IList<IntraDayIncome> IntraDayIncomes { get; set; }
}

