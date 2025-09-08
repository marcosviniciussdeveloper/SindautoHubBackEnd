using SindautoHub.Application.Dtos.PositionDtos;
using SindautoHub.Application.Interface;

public class PositionServices : IPositionServices
{
    public Task<PositionResponse> CreateAsync(CreatePositionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PositionResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PositionResponse> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PositionResponse> UpdateAsync(Guid id, UpdatePositionRequest request)
    {
        throw new NotImplementedException();
    }
}
