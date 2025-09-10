using Microsoft.AspNetCore.Mvc;
using SindautoHub.Application.Interface;
using SindautoHub.Application.Dtos.ChatMessageDtos;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatServices _chatService;

    public ChatController(IChatServices chatService)
    {
        _chatService = chatService;
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
}
