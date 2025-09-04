
using SindautoHub.Application.Dtos;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public interface IFuncionarioServices
    {
        Task<FuncionarioResponseDto> CreateAsync (CreateFuncionarioRequest createRequest);

        Task<bool> DeleteAsync(Guid FuncionarioId);
        Task<IEnumerable<Funcionario>> GetAllAsync(Guid FuncionarioId);

        Task<Funcionario> UpdateAsync (Guid id , UpdateFuncionarioRequest updateRequest);
        Task<Funcionario> GetByIdAsync (Guid FuncionarioId );
    }
}
