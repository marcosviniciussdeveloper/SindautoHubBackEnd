
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public interface IUsersRespository
    {
        Task <User> CreateAsync (User funcionario);

        Task<bool> DeleteAsync(Guid  funcionarioId);  

        Task <User> GetByIdAsync (Guid funcionarioId);

        Task<IEnumerable<User>> GetAllAsync(Guid FuncionarioId);

        Task <User> UpdateAsync (User FuncionarioId);
         Task<User> GetByCpfAsync(string cpf);
        Task <User?> GetByEmailAsync (string email);

        Task<User?> GetByIdWithincludesAsync(Guid id);
    }
}
