﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Services.Utilities.Results.Interfaces;

namespace Epias.Services.Utilities.Results.Interfaces;

//out ile yazınca T entity de olabilir  bir entity list de olabilir. Örnek kullanımlar altta var.
public interface IDataResult<out T> : IResult
{
    public T Data { get; } //new DataResult<Category>(ResultStatus.success,category);//new DataResult<IList<Category>>(ResultStatus.success,categoryList);
}
