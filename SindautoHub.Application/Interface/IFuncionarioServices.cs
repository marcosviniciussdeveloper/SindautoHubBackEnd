
using SindautoHub.Application.Dtos;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public interface IFuncionarioServices_cs
    {
        Task<Funcionario> CreateAsync (CreateFuncionarioRequest createRequest);

        Task<bool> DeleteAsync(Guid FuncionarioId);
        Task<IEnumerable<Funcionario>> GetAllAsync(Guid FuncionarioId);

        Task<Funcionario> UpdateAsync (Guid id , UpdateFuncionarioRequest updateRequest);
        Task<Funcionario> GetByIdAsync (Guid FuncionarioId );
    }
}
