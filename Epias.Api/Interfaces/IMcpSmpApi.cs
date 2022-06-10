using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Models;
using Epias.Models;

namespace Epias.Transparency.Api.Interfaces;

public interface IMcpSmpApi
{
    Task<List<McpSmp>> GetAll();
}

