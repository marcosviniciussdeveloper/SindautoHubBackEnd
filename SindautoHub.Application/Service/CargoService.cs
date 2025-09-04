using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Application.Dtos;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Application.Service
{
    public class CargoServices : ICargoServices
    {
        public Task<Cargo> CreateAsync(CreateCargoRequest CreateRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid cargoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CargoResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CargoResponse?> GetByIdAsync(Guid cargoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Guid cargoId, UpdateCargoRequest updateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
