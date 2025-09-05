using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public interface IAnnouncementsRepository
    {
        Task<announcements> CreateAsync (announcements notificacao );


        Task<bool> DeleteAsync(announcements notificacao);

        Task<announcements> UpdateAsync (announcements notificacaoId);



        Task<announcements> GetByIdAsync (Guid notificacaoId);

        Task<IEnumerable<announcements>> GetAllAsync (Guid FuncionarioId);
    }
}
