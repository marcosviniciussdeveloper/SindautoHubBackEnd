using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using System.Security.Claims;

namespace SindautoHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificacoesController : ControllerBase
{
    private readonly INotificacaoServices _notificacaoService;


    public NotificacoesController(INotificacaoServices notificacaoService)
    {
        _notificacaoService = notificacaoService;
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNotificacaoRequest createRequest)
    {
        var novaNotificacao = await _notificacaoService.CreateAsync(createRequest);
        return CreatedAtAction(nameof(GetById), new { notificacaoId = novaNotificacao.Id }, novaNotificacao);
    }

    
    [HttpGet("{notificacaoId}")]
    public async Task<IActionResult> GetById(Guid notificacaoId)
    {
        var notificacao = await _notificacaoService.GetByIdAsync(notificacaoId);
        if (notificacao is null)
        {
            return NotFound();
        }
        return Ok(notificacao);
    }

   
    [HttpGet("minhas")]
    public async Task<IActionResult> GetMinhasNotificacoes()
    {
        var usuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var notificacoes = await _notificacaoService.GetAllByUsuarioIdAsync(usuarioId);
        return Ok(notificacoes);
    }


    [HttpPost("marcar-como-lida/{notificacaoId}")] 
    public async Task<IActionResult> MarkAsRead(Guid notificacaoId)
    {
        var success = await _notificacaoService.MarkAsReadAsync(notificacaoId);
        if (!success)
        {
            return NotFound("Notificação não encontrada.");
        }
        return NoContent();
    }

  
    [HttpPost("marcar-todas-como-lidas")] 
    public async Task<IActionResult> MarkAllAsRead()
    {
        var usuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _notificacaoService.MarkAllAsReadAsync(usuarioId);
        return NoContent();
    }

    [HttpDelete("{notificacaoId}")]
    public async Task<IActionResult> Delete(Guid notificacaoId)
    {
        var success = await _notificacaoService.DeleteAsync(notificacaoId);
        if (!success)
        {
            return NotFound("Notificação não encontrada.");
        }
        return NoContent();
    }
}