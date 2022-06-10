using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Data.Interfaces;
using Epias.Entities.Dtos;
using Epias.Models;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Epias.Services.Utilities.Results.Concrete;
using Epias.Services.Utilities.Results.Interfaces;

namespace Epias.Services.Services;

public class McpSmpManager:IMcpSmpService
{
    private readonly IMcpSmpRepository _mcpSmpRepository;

    public McpSmpManager(IMcpSmpRepository mcpSmpRepository)
    {
        _mcpSmpRepository = mcpSmpRepository;
    }
    public async Task<IResult> AddListAsync(List<McpSmp> mcpSmps)
    {

        foreach (var mcpSmp in mcpSmps)
        {
            var result = await _mcpSmpRepository.Get(t => t.Date == mcpSmp.Date);
            if (result != null)
            {
                mcpSmp.Id = result.Id;
                await _mcpSmpRepository.UpdateAsync(mcpSmp);
            }
            else
            {
                await _mcpSmpRepository.AddAsync(mcpSmp);
            }
        }
        await _mcpSmpRepository.SaveChangesAsync();
        return new Result(ResultStatus.Success);
    }

    public async Task<IDataResult<McpSmpListDto>> GetAllByDateAsync()
    {
        var mcpSmps = await _mcpSmpRepository.GetAllByDateAsync();
        await _mcpSmpRepository.SaveChangesAsync();
        return new DataResult<McpSmpListDto>(ResultStatus.Success, new McpSmpListDto
        {
            McpSmps = mcpSmps
        });
    }
}

