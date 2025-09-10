using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SindautoHub.Api.Hubs
{
    public class ChatHub : Hub
    {
       
        private static readonly ConcurrentDictionary<Guid, string> ConnectedUsers = new();

        public override async Task OnConnectedAsync()
        {
            var userIdClaim = Context.User?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (Guid.TryParse(userIdClaim, out var userId))
            {
                ConnectedUsers[userId] = Context.ConnectionId;
                await Clients.Caller.SendAsync("Connected", $"Usuário {userId} conectado ao ChatHub.");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userIdClaim = Context.User?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (Guid.TryParse(userIdClaim, out var userId))
            {
                ConnectedUsers.TryRemove(userId, out _);
            }

            await base.OnDisconnectedAsync(exception);
        }

        
        public async Task JoinGroup(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            await Clients.Group(chatId).SendAsync("SystemMessage", $"Um usuário entrou no chat {chatId}.");
        }

   
        public async Task LeaveGroup(string chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
            await Clients.Group(chatId).SendAsync("SystemMessage", $"Um usuário saiu do chat {chatId}.");
        }
    }
}
