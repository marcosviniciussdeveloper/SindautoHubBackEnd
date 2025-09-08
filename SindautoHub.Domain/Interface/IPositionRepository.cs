using SindautoHub.Domain.Entities.Models;

public interface IPositionRepository
{ 
Task<Position?> GetByIdAsync(Guid positionId);
    Task<IEnumerable<Position>> GetAllAsync();
    Task CreateAsync(Position position);
    Task UpdateAsync(Position position);
    Task DeleteAsync(Position position);
}
