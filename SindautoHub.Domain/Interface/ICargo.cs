using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public interface ICargo
    {
        Task <Cargo> CreateAsync (Cargo cargo);
        Task <bool> DeleteAsync  (Cargo Cargoid);

        Task <Cargo> UpdateAsync (Cargo cargo);


    }
}
