
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public interface IFuncionarioRespository
    {
        Task <Funcionario> CreateAsync (Funcionario funcionario);

        Task<bool> DeleteAsync(Guid  funcionarioId);  

        Task <Funcionario> GetByIdAsync (Guid funcionarioId);

        Task<IEnumerable<Funcionario>> GetAllAsync(Guid FuncionarioId);

        Task <Funcionario> UpdateAsync (Funcionario FuncionarioId);
         Task<Funcionario> GetByCpfAsync(string cpf);
        Task <Funcionario?> GetByEmailAsync (string email);

        Task<Funcionario?> GetByIdWithincludesAsync(Guid id);
    }
}
