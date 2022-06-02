using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Services.Utilities.Results.ComplexTypes;


namespace Epias.Services.Utilities.Results.Interfaces;

public interface IResult
{
    public ResultStatus ResultStatus { get; } //ResultStatus.Succes //ResultStatus.Error
    public string Message { get; }
    public Exception Exception { get; }
    //IEnumerable yapma nedenimiz daha sonradan ValidationErrors.Add şeklinde ekleme yapılmaması. Başta oluşturulduğu gibi kalıyor.
}

