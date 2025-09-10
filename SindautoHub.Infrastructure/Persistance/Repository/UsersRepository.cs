using Microsoft.EntityFrameworkCore;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Interfaces;
using SindautoHub.Infrastructure.Persistance.Database;
 

namespace SindautoHub.Infrastructure.Persistence.Repository;

public class UserRepository : IUserRepository
{
    private readonly SindautoHubContext _context;

    public UserRepository(SindautoHubContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Position)
            .Include(u => u.Sector)
            .ToListAsync();
    }

    public async Task<User?> GetByCpfAsync(string cpf)
    {
        return await _context.Users
                             .FirstOrDefaultAsync(u => u.Cpf == cpf);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
                             .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User?> GetByIdWithDetailsAsync(Guid userId)
    {
        return await _context.Users
                             .Include(u => u.Sector)
                             .Include(u => u.Position)
                             .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetByNameAsync(string UserName)
    {
        return await _context.Users.FirstOrDefaultAsync(u =>  u.UserName == UserName);
    }

    public async Task<User?> GetByWhatsappNumberAsync(string whatsappNumber)
    {
        return await _context.Users
                             .FirstOrDefaultAsync(u => u.WhatsappNumber == whatsappNumber);
    }

    public Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }
}