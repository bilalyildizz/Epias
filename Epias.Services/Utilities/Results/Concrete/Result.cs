using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Services.Utilities.Results.ComplexTypes;
using Epias.Services.Utilities.Results.Interfaces;

namespace Epias.Services.Utilities.Results.Concrete;

public class Result : IResult
{
    // new Result(REsultStatus.Error, exception.message, exception)   Örnek Kullanımı.
    public Result(ResultStatus resultStatus)
    {
        ResultStatus = resultStatus;
    }

    public Result(ResultStatus resultStatus, string message)
    {
        ResultStatus = resultStatus;
        Message = message;
    }

    public Result(ResultStatus resultStatus, string message, Exception exception)
    {
        ResultStatus = resultStatus;
        Message = message;
        Exception = exception;
    }

    public ResultStatus ResultStatus { get; }
    public string Message { get; }
    public Exception Exception { get; }
}

