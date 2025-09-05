
using SindautoHub.Application.Dtos;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public interface IFuncionarioServices
    {
        Task<FuncionarioResponseDto> CreateAsync (CreateFuncionarioRequest createRequest);

        Task<bool> DeleteAsync(Guid FuncionarioId);
        Task<IEnumerable<User>> GetAllAsync(Guid FuncionarioId);

        Task<bool> UpdateAsync (Guid FuncionarioID , UpdateFuncionarioRequest updateRequest);
        Task<FuncionarioResponseDto> GetByIdAsync (Guid FuncionarioId );
    }
}
