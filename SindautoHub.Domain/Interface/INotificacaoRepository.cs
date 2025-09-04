using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public interface INotificacaoRepository
    {
        Task<Notificacao> CreateAsync (Notificacao notificacao );


        Task<bool> DeleteAsync(Guid notificacaoId);

        Task<Notificacao> UpdateAsync (Notificacao notificacaoId);


        Task<Notificacao> GetByIdAsync (Guid notificacaoId);

        Task<IEnumerable<Notificacao>> GetAllAsync (Guid FuncionarioId);
    }
}
