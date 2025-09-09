using Microsoft.EntityFrameworkCore;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interface;
using SindautoHub.Domain.Interfaces;
using SindautoHub.Infrastructure.Persistance.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SindautoHub.Infrastructure.Persistence.Repository;

public class SectorRepository : ISectorRepository
{
    private readonly SindautoHubContext _context;

    public SectorRepository(SindautoHubContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Sector sector)
    {
        await _context.Sectors.AddAsync(sector);
    }

    public Task DeleteAsync(Sector sector)
    {
        _context.Sectors.Remove(sector);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Sector>> GetAllAsync()
    {
        return await _context.Sectors
            .AsNoTracking()
            .Include(s => s.Users)
            .ToListAsync();
    }


    public async Task<Sector?> GetByIdAsync(Guid sectorId)
    {
        return await _context.Sectors.FindAsync(sectorId);
    }

    public Task UpdateAsync(Sector sector)
    {
        _context.Sectors.Update(sector);
        return Task.CompletedTask;
    }
}