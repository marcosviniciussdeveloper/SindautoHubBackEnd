using Microsoft.AspNetCore.Mvc;
using SindautoHub.Application.Interface;

[ApiController]
[Route("api/[controller]")]
public class PresenceController : ControllerBase
{
    private readonly IPresenceService _presence;

    public PresenceController(IPresenceService presence)
    {
        _presence = presence;
    }

    [HttpGet("{userId}/status")]
    public async Task<IActionResult> GetUserStatus(Guid userId)
    {
        var isOnline = await _presence.IsUserOnlineAsync(userId);
        return Ok(new { userId, status = isOnline ? "Online" : "Offline" });
    }

    [HttpGet("sector/{sectorId}/online")]
    public async Task<IActionResult> GetOnlineUsers(Guid sectorId)
    {
        var onlineUsers = await _presence.GetOnlineUsersBySectAsync(sectorId);
        return Ok(new { sectorId, onlineUsers });
    }
}
