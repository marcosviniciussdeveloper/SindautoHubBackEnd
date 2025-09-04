using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public  interface ISetoresRepository
    {
        Task <Setor> CreateAsync (Setor setores);
        Task<bool> DeleteAsync(Guid setoresId);
        Task <Setor> GetByIdAsync (Guid setoresId);
        Task<IEnumerable<Setor>> GetAllAsync(Guid setoresId);
        Task <Setor> UpdateAsync (Setor  SetorId );

    }
}
