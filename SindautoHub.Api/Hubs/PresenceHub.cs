using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SindautoHub.Application.Interface;

namespace SindautoHub.Api.Hubs
{
    [Authorize] // só usuários autenticados podem se conectar
    public class PresenceHub : Hub
    {
        private readonly IPresenceService _presence;

        public PresenceHub(IPresenceService presence)
        {
            _presence = presence;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst("sub")?.Value;
            var sectorId = Context.GetHttpContext()?.Request.Query["sectorId"].ToString();

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(sectorId))
                await _presence.SetOnlineAsync(Guid.Parse(userId), Guid.Parse(sectorId));

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst("sub")?.Value;
            var sectorId = Context.GetHttpContext()?.Request.Query["sectorId"].ToString();

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(sectorId))
                await _presence.SetOfflineAsync(Guid.Parse(userId), Guid.Parse(sectorId));

            await base.OnDisconnectedAsync(exception);
        }

        public async Task Ping()
        {
            var userId = Context.User?.FindFirst("sub")?.Value;
            if (!string.IsNullOrEmpty(userId))
                await _presence.PingAsync(Guid.Parse(userId));
        }

        public async Task SetAusente()
        {
            var userId = Context.User?.FindFirst("sub")?.Value;
            if (!string.IsNullOrEmpty(userId))
                await _presence.SetAusenteAsync(Guid.Parse(userId));
        }
    }
}
