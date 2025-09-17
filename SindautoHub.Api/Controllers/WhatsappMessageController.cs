using Microsoft.AspNetCore.Mvc;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;

[ApiController]
[Route("api/[controller]")]
public class WhatsappController : ControllerBase
{
    private readonly IWhatsappService _whatsappService;

    public WhatsappController(IWhatsappService whatsappService)
    {
        _whatsappService = whatsappService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] WhatsappMessageDto dto)
    {
        await _whatsappService.SendMessageAsync(dto.Phone, dto.Message);
        return Ok("Mensagem enviada.");
    }
}
