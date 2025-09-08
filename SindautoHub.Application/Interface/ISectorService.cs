using SindautoHub.Application.Dtos.SectorDtos;

namespace SindautoHub.Application.Interface
{
    public interface ISectorService
    {
        Task<SectorResponse> CreateAsync(CreateSectorRequest request);
        Task<SectorResponse> UpdateAsync(Guid id, UpdateSectorRequest request);
        Task<SectorResponse> GetByIdAsync(Guid id);
        Task<List<SectorResponse>> GetAllAsync();
        Task<bool> DeleteAsync(Guid id);
    }
}
