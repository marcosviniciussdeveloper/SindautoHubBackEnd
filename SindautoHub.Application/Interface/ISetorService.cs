using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Application.Dtos;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Application.Interface
{
    public interface ISetorService
    {
        Task <Setor> CreateAsync (CreateSetorRequest createSetorRequest);

        Task<bool> DeleteAsync(Guid setoresId);

        Task<Setor> GetByIdAsync (Guid setoresId);

        Task<IEnumerable<Setor>> GetAllAsync(Guid setoresId);

        Task <Setor> UpdateAsync (Guid SetorId, UpdateSetorRequest updateSetorRequest);


    }
}
