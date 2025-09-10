namespace SindautoHub.Api.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using SindautoHub.Application.Interface;

    public class PresenceHub : Hub
    {
        private readonly IPresenceService _presence;
        public PresenceHub(IPresenceService presence) => _presence = presence;

        public override async Task OnConnectedAsync()
        {
            var userId = Guid.Parse(Context.User!.FindFirst("sub")!.Value);
            var sectorId = Guid.Parse(Context.GetHttpContext()!.Request.Query["sectorId"]!);

            await _presence.SetOnlineAsync(userId, sectorId);
            await Groups.AddToGroupAsync(Context.ConnectionId, sectorId.ToString());
            await Clients.Group(sectorId.ToString())
                .SendAsync("presenceChanged", new { userId, status = "Online" });

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? ex)
        {
            var userId = Guid.Parse(Context.User!.FindFirst("sub")!.Value);
            var sectorId = Guid.Parse(Context.GetHttpContext()!.Request.Query["sectorId"]!);

            await _presence.SetOfflineAsync(userId, sectorId);
            await Clients.Group(sectorId.ToString())
                .SendAsync("presenceChanged", new { userId, status = "Offline" });

            await base.OnDisconnectedAsync(ex);
        }

        public Task Ping()
        {
            var userId = Guid.Parse(Context.User!.FindFirst("sub")!.Value);
            return _presence.PingAsync(userId);
        }
    }
}