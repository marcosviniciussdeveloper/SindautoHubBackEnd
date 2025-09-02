
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public interface ICargoRepository
    {
        Task <Cargo> CreateAsync (Cargo cargo);
        Task<bool> DeleteAsync(Guid CargoId);

        Task <Cargo> GetByIdAsync (Guid CargoId );
        Task<IEnumerable<Cargo>> GetAllAsync(Guid  CargosId );

        Task <Cargo> UpdateAsync (Guid CargoId);


    }
}
