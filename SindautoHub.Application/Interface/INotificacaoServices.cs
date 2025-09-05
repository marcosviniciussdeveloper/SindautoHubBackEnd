using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Application.Dtos;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Application.Interface
{
    public interface INotificacaoServices
    {
        Task<NotificacaoResponse> CreateAsync(CreateNotificacaoRequest createRequest);
        Task<IEnumerable<NotificacaoResponse>> GetAllByUsuarioIdAsync(Guid usuarioId);
        Task<bool>MarkAsReadAsync (Guid notificacaoId);

        Task <NotificacaoResponse> GetByIdAsync (Guid notificacaoId);
        Task<bool> MarkAllAsReadAsync(Guid usuarioId);
        
        Task<bool> DeleteAsync (Guid noitificaoId);

    }
}
