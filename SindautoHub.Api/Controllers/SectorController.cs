using Microsoft.AspNetCore.Mvc;
using SindautoHub.Application.Dtos.SectorDtos;
using SindautoHub.Application.Interface;

namespace SindautoHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectorController : ControllerBase
{
    private readonly ISectorService _sectorService;

    public SectorController(ISectorService sectorService)
    {
        _sectorService = sectorService;
    }

    // GET: api/sector
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sectorService.GetAllAsync();
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

        return Ok(new
        {
            message = "Setor excluído com sucesso."
        });
    }
}
