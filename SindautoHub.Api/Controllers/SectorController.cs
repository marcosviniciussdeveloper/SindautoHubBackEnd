using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SindautoHub.Application.Dtos.SectorDtos;
using SindautoHub.Application.Interface;
using System.Text.Json;

namespace SindautoHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectorController : ControllerBase
{
    private readonly ISectorService _sectorService;
    private readonly ICacheService _cache;
    private static readonly JsonSerializerOptions JsonOpts = new(JsonSerializerDefaults.Web);
    private const string CacheKey = "sectors_all";

    public SectorController(ISectorService sectorService, ICacheService cache)
    {
        _sectorService = sectorService;
        _cache = cache;
    }

    // GET: api/sector
    [HttpGet]
 
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        // tenta pegar do cache
        var cached = await _cache.GetAsync(CacheKey);
        if (!string.IsNullOrWhiteSpace(cached))
        {
            var sectors = JsonSerializer.Deserialize<List<SectorResponse>>(cached, JsonOpts) ?? new();
            return Ok(new { message = "ok (cache)", data = sectors });
        }

        // se não tem no cache → busca do serviço
        var result = await _sectorService.GetAllAsync();

        var json = JsonSerializer.Serialize(result, JsonOpts);
        await _cache.SetAsync(
            CacheKey,
            json,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            },
            ct);

        return Ok(new { message = "ok", data = result });
    }


    // GET: api/sector/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var sector = await _sectorService.GetByIdAsync(id);
        if (sector == null)
            return NotFound(new { message = "Setor não encontrado." });

        return Ok(sector);
    }

    // POST: api/sector
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSectorRequest request)
    {
        var created = await _sectorService.CreateAsync(request);

        // Invalida cache
        await _cache.RemoveAsync(CacheKey);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, new
        {
            message = "Setor criado com sucesso!",
            data = created
        });
    }

    // PUT: api/sector/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSectorRequest request)
    {
        var updated = await _sectorService.UpdateAsync(id, request);
        if (updated == null)
            return NotFound(new { message = "Setor não encontrado para atualizar." });

        await _cache.RemoveAsync(CacheKey);

        return Ok(new
        {
            message = "Setor atualizado com sucesso!",
            data = updated
        });
    }

    // DELETE: api/sector/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _sectorService.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = "Setor não encontrado para exclusão." });

        await _cache.RemoveAsync(CacheKey);

        return Ok(new
        {
            message = "Setor excluído com sucesso."
        });
    }



}
