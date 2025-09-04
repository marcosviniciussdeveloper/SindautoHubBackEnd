using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Application.Service
{
    public class SetorService : ISetorService
    {
        public Task<Setor> CreateAsync(CreateSetorRequest createSetorRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid setoresId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Setor>> GetAllAsync(Guid setoresId)
        {
            throw new NotImplementedException();
        }

        public Task<Setor> GetByIdAsync(Guid setoresId)
        {
            throw new NotImplementedException();
        }

        public Task<Setor> UpdateAsync(Guid SetorId, UpdateSetorRequest updateSetorRequest)
        {
            throw new NotImplementedException();
        }
    }
}
