using Microsoft.AspNetCore.Mvc;
using SindautoHub.Application.Dtos.ChatDtos;
using SindautoHub.Application.Dtos.ChatMessageDtos;
using SindautoHub.Application.Interface;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatServices _chatService;
    private readonly IPresenceService _presence;
    public ChatController(IPresenceService presenceService,IChatServices chatService)
    {
        _presence = presenceService;
        _chatService = chatService;
    }

    [HttpGet("sector/{sectorId}/online")]
    public async Task<IActionResult> GetOnlineUsers(Guid sectorId)
    {
        var onlineUsers = await _presence.GetOnlineUsersBySectAsync(sectorId);
        return Ok(new { sectorId, onlineUsers });
    }


    [HttpPost("{chatId}/messages")]
    public async Task<IActionResult> SendMessage(Guid chatId, [FromBody] SendChatMessageRequest request)
    {
        
        var senderId = Guid.Parse(User.FindFirst("sub")?.Value ?? throw new UnauthorizedAccessException());

        var response = await _chatService.SendMessageAsync(chatId, senderId, request);

        return Ok(new
        {
            message = "Mensagem enviada com sucesso!",
            data = response
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatRequest request)
    {
        var chat = await _chatService.CreateChatAsync(request);
        return Ok(chat);


    }
}
