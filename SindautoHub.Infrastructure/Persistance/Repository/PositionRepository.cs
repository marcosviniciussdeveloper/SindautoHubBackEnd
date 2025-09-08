using Microsoft.EntityFrameworkCore;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Infrastructure.Persistance.Database;

namespace SindautoHub.Infrastructure.Persistence.Repository;

public class PositionRepository : IPositionRepository
{
    private readonly SindautoHubContext _context;

    public PositionRepository(SindautoHubContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Position position)
    {
        await _context.Positions.AddAsync(position);
    }

    public Task DeleteAsync(Position position)
    {
        _context.Positions.Remove(position);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Position>> GetAllAsync()
    {
        return await _context.Positions.ToListAsync();
    }

    public async Task<Position?> GetByIdAsync(Guid positionId)
    {
        return await _context.Positions.FindAsync(positionId);
    }



    public Task UpdateAsync(Position position)
    {
        _context.Positions.Update(position);
        return Task.CompletedTask;
    }
}