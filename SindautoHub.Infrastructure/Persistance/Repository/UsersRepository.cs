using Microsoft.EntityFrameworkCore;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Enums;
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

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Position)
            .Include(u => u.Sector)
            .Where(u => u.Status == Status.Ativo)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetBySectorIdWithDetailsAsync(Guid sectorId)
    {
        return await _context.Users
            .Include(u => u.Position)
            .Where(u => u.SectorId == sectorId && u.Status == Status.Ativo)
            .ToListAsync();
    }

    public async Task<User?> GetByCpfAsync(string cpf)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Cpf == cpf && u.Status == Status.Ativo);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.Status == Status.Ativo);
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _context.Users
            .Where(u => u.Id == userId && u.Status == Status.Ativo)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetByIdWithDetailsAsync(Guid userId)
    {
        return await _context.Users
            .Include(u => u.Sector)
            .Include(u => u.Position)
            .FirstOrDefaultAsync(u => u.Id == userId && u.Status == Status.Ativo);
    }

    public async Task<User?> GetByNameAsync(string userName)
    {
        return await _context.Users
            .Include(u => u.Sector)
            .Include(u => u.Position)
            .FirstOrDefaultAsync(u => u.UserName == userName && u.Status == Status.Ativo);
    }

    public async Task<User?> GetByWhatsappNumberAsync(string whatsappNumber)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.WhatsappNumber == whatsappNumber && u.Status == Status.Ativo);
    }


    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }
    public Task DeleteAsync(User user)
    {

        user.Status = Status.Inativo;
        user.UpdatedAt = DateTime.UtcNow;
        _context.Users.Update(user);
        return Task.CompletedTask;

    }
    }