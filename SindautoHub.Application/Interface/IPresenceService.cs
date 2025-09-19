using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Domain.Entities.Enums;

namespace SindautoHub.Application.Interface
{
    public interface IPresenceService
    {
        Task SetOnlineAsync(Guid Id  ,Guid Sector);
        Task SetOfflineAsync(Guid userId, Guid sectorId);

        Task PingAsync(Guid userId);         
        Task SetAusenteAsync(Guid userId);    
        
        Task<List<Guid>>GetOnlineUsersBySectAsync(Guid userId);

        Task<bool> IsUserOnlineAsync(Guid userId);

        Task<PresenceStatus?> GetStatusAsync(Guid userId);
        Task<int> GetOnlineCountAsync(Guid sectorId);
    }
}
