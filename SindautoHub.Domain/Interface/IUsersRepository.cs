using SindautoHub.Domain.Entities; 

namespace SindautoHub.Domain.Interfaces;

public interface IUserRepository
{
   
    Task<User?> GetByIdAsync(Guid userId);

    Task<User?> GetByIdWithDetailsAsync(Guid userId);

    Task<IEnumerable<User>> GetAllAsync();

    Task<User?> GetByCpfAsync(string cpf);

    Task<User?> GetByNameAsync(string UserName);
    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByWhatsappNumberAsync(string whatsappNumber);

    Task CreateAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(User user);
}