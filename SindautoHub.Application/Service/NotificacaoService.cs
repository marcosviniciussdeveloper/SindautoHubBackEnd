using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;

namespace SindautoHub.Application.Service
{
    public class NotificacaoService : INotificacaoServices
    {
        public Task<NotificacaoResponse> CreateAsync(CreateNotificacaoRequest createRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid notificacaoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NotificacaoResponse>> GetAllByUsuarioIdAsync(Guid usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkAllAsReadAsync(Guid usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkAsReadAsync(Guid notificacaoId)
        {
            throw new NotImplementedException();
        }
    }
}
