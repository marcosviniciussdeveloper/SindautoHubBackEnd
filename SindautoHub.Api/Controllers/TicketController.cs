using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SindautoHub.Application.Dtos.TicketDtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ICacheService _cacheService;
        private static readonly JsonSerializerOptions JsonOpts = new(JsonSerializerDefaults.Web);

        public TicketController(ITicketService ticketService, ICacheService cacheService)
        {
            _ticketService = ticketService;
            _cacheService = cacheService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
        {
            var userId = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString());

            var ticket = await _ticketService.CreateAsync(request, userId);

            return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, new
            {
                message = "Ticket criado com sucesso.",
                data = ticket
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            const string cacheKey = "tickets_all";
            var cached = await _cacheService.GetAsync(cacheKey);

            if (!string.IsNullOrWhiteSpace(cached))
            {
                var fromCache = JsonSerializer.Deserialize<List<TicketResponse>>(cached, JsonOpts) ?? new();
                return Ok(new
                {
                    message = $"Tickets carregados do cache. Total: {fromCache.Count}",
                    data = fromCache
                });
            }

            var result = (await _ticketService.GetAllAsync()).ToList();


            var json = JsonSerializer.Serialize(result, JsonOpts);
            await _cacheService.SetAsync(
             cacheKey,
                 json,
             new DistributedCacheEntryOptions
                 {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
     }
 );

            return Ok(new
            {
                message = $"Tickets carregados do banco. Total: {result.Count}",
                data = result
            });
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cacheKey = $"ticket_{id}";
            var cached = await _cacheService.GetAsync(cacheKey);

            if (!string.IsNullOrWhiteSpace(cached))
            {
                var ticket = JsonSerializer.Deserialize<TicketResponse>(cached, JsonOpts);
                return Ok(new
                {
                    message = "Ticket carregado do cache.",
                    data = ticket
                });
            }

            var result = await _ticketService.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Ticket não encontrado." });

            // Salvar no cache
            var json = JsonSerializer.Serialize(result, JsonOpts);
            await _cacheService.SetAsync(
            cacheKey,
                json,
             new DistributedCacheEntryOptions
                {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
             }
            );

            return Ok(new
            {
                message = "Ticket carregado do banco.",
                data = result
            });
        }

        [HttpPut("{id}/assign")]
        [Authorize]
        public async Task<IActionResult> AssignAgent(Guid id, [FromBody] AssignTicketRequest request)
        {
            var updated = await _ticketService.AssignTicketAsync(id, request.AgentId);
            return Ok(new
            {
                message = "Ticket atribuído com sucesso.",
                data = updated
            });
        }

        [HttpPut("{id}/status")]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateTicketStatusRequest request)
        {
            var updated = await _ticketService.UpdateStatusAsync(id, request.NewStatus);
            return Ok(new
            {
                message = "Status do ticket atualizado.",
                data = updated
            });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _ticketService.DeleteAsync(id);
            return Ok(new
            {
                message = "Ticket deletado com sucesso."
            });
        }
    }
}
