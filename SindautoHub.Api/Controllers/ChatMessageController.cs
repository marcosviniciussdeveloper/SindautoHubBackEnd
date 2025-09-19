using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SindautoHub.Application.Dtos.ChatMessageDtos;
using SindautoHub.Application.Interface;

namespace SindautoHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatMessageController(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        [HttpPost("{chatId}/messages")]
        public async Task<IActionResult> SendMessage(Guid chatId, [FromBody] SendChatMessageRequest request)
        {
       
            var senderIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
           if (string.IsNullOrEmpty(senderIdClaim))
                return Unauthorized(new { message = "Usuário não autenticado." });

            var senderId = Guid.Parse(senderIdClaim);

            var response = await _chatMessageService.SendMessageAsync(chatId, senderId, request);

            return Ok(new
            {
                message = "Mensagem enviada com sucesso!",
                data = response
            });
        }

        [HttpGet("{chatId}/messages")]
        public async Task<IActionResult> GetMessages(Guid chatId)
        {
            var messages = await _chatMessageService.GetMessagesByChatIdAsync(chatId);

            return Ok(new
            {
                message = $"Total de mensagens encontradas: {messages.Count}",
                data = messages
            });
        }
    }
}
