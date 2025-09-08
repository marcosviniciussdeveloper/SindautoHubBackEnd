using SindautoHub.Application.Dtos.SectorDtos;
using SindautoHub.Application.Interface;

namespace SindautoHub.Application.Service;
public class SectorService : ISectorService
{
    public Task<SectorResponse> CreateAsync(CreateSectorRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<SectorResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SectorResponse> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<SectorResponse> UpdateAsync(Guid id, UpdateSectorRequest request)
    {
        throw new NotImplementedException();
    }
}