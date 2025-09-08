
using SindautoHub.Domain.Entities.Models;

public interface ISectorRepository
{
    Task<Sector?> GetByIdAsync(Guid sectorId);
    Task<IEnumerable<Sector>> GetAllAsync();
    Task CreateAsync(Sector sector);
    Task UpdateAsync(Sector sector);
    Task DeleteAsync(Sector sector);
}

   
