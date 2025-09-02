using SindautoHub.Application.Dtos;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public interface ICargoServices 
    {
        Task<Cargo> CreateAsync(CreateCargoRequest CreateRequest);
        Task<bool> DeleteAsync(Guid cargoId );  
        Task <CargoResponse?> GetByIdAsync (Guid cargoId);
        Task<IEnumerable<CargoResponse>> GetAllAsync();
        Task<bool> UpdateAsync(Guid cargoId, UpdateCargoRequest updateRequest);
    }
}
