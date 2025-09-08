using SindautoHub.Application.Dtos.PositionDtos;

namespace SindautoHub.Application.Interface
{
    public interface IPositionServices
    {
        Task<PositionResponse> CreateAsync(CreatePositionRequest request);
        Task<PositionResponse> UpdateAsync(Guid id, UpdatePositionRequest request);
        Task<PositionResponse> GetByIdAsync(Guid id);
        Task<List<PositionResponse>> GetAllAsync();
        Task<bool> DeleteAsync(Guid id);
    }
}
