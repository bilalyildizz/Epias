using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Models;

namespace Epias.Transparency.Api.Interfaces;

public interface IIntraDayVolumeApi
{
    Task<List<IntraDayVolume>> GetAll();
}

