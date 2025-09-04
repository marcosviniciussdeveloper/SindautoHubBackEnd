using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public interface IPostagensRepository
    {
        Task <Postagem> CreateAsync (Postagem postagem);

        Task <IEnumerable<Postagem>> GetAllAsync(Guid postagemId);


        Task <Postagem > GetByIdAsync (Guid postagemId);

        Task <Postagem> UpdateAsync (Postagem postagemId);    

        Task<bool> DeleteAsync(Postagem postagemId);
    }
}
