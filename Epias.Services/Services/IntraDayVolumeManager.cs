using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Data.Interfaces;
using Epias.Entities.Dtos;
using Epias.Entities.Models;
using Epias.Services.Interfaces;
using Epias.Services.Utilities.Results.ComplexTypes;
using Epias.Services.Utilities.Results.Concrete;
using Epias.Services.Utilities.Results.Interfaces;

namespace Epias.Services.Services;

public class IntraDayVolumeManager : IIntraDayVolumeService
{
    private readonly IIntraDayVolumeRepository _intraDayVolumeRepository;
    public IntraDayVolumeManager(IIntraDayVolumeRepository intraDayVolumeRepository)
    {
        _intraDayVolumeRepository = intraDayVolumeRepository;
    }

    public async Task<IResult> AddListAsync(List<IntraDayVolume> intraDayVolumes)
    {
        foreach (var intraDayVolume in intraDayVolumes)
        {
            var result = await _intraDayVolumeRepository.AnyAsync(t=>t.BlockMatchQuantity==intraDayVolume.BlockMatchQuantity&&t.Date==intraDayVolume.Date&&t.HourlyMatchQuantity==intraDayVolume.HourlyMatchQuantity);
            if (!result)
            {
                await _intraDayVolumeRepository.AddAsync(intraDayVolume);
            }
        }
        await _intraDayVolumeRepository.SaveChangesAsync();
        return new Result(ResultStatus.Success);
    }

    public async Task<IDataResult<IntraDayVolumeListDto>> GetAllByDateAsync()
    {
        var intraDayVolumes = await _intraDayVolumeRepository.GetAllByDateAsync(t => t.Date.Date == DateTime.Today);
        await _intraDayVolumeRepository.SaveChangesAsync();
        return new DataResult<IntraDayVolumeListDto>(ResultStatus.Success, new IntraDayVolumeListDto
        {
            IntraDayVolumes = intraDayVolumes
        });
    }
}

