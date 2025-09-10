using Microsoft.AspNetCore.SignalR;
using SindautoHub.Api.Hubs;
using SindautoHub.Application.Interface;

public class SignalRChatNotifier : IChatNotifier
{
    private readonly IHubContext<ChatHub> _hub;

    public SignalRChatNotifier(IHubContext<ChatHub> hub)
    {
        _hub = hub;
    }

    public async Task NotifyMessageAsync(Guid chatId, object message)
    {
        await _hub.Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", message);
    }
}
