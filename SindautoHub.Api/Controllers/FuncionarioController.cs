using Microsoft.AspNetCore.Mvc;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Service;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioService _funcionarioService;

        public FuncionarioController(FuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService ?? throw new ArgumentNullException(nameof(funcionarioService));
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateFuncionarioRequest createRequest)
        {
            var novoFuncionario = await _funcionarioService.CreateAsync(createRequest);
            return CreatedAtAction(nameof(Create), new { id = novoFuncionario.Id }, novoFuncionario);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var funcionario = await _funcionarioService.GetByIdAsync(id);
            return Ok(funcionario);
        }


        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFuncionarioRequest updateRequest)
        {
            var funcionarioAtualizado = await _funcionarioService.UpdateAsync(id, updateRequest);
            if (funcionarioAtualizado == null)
            {
                return NotFound();
            }
            return Ok(funcionarioAtualizado);


        }

    }
    }


