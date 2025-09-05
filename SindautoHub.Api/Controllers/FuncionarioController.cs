using Microsoft.AspNetCore.Mvc;
using SindautoHub.Application.Dtos;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioServices _funcionarioService;

        public FuncionarioController(IFuncionarioServices funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateFuncionarioRequest createRequest)
        {
            var novoFuncionario = await _funcionarioService.CreateAsync(createRequest);
            return CreatedAtAction(nameof(Create), new  {  id =  novoFuncionario  , message = "Funcionario cadastrado com Sucesso!"},  novoFuncionario);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var funcionario = await _funcionarioService.GetByIdAsync(id);
            return Ok(funcionario);
        }


        [HttpPatch]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFuncionarioRequest  request)
        {
            var success = await _funcionarioService.UpdateAsync(id, request);
            if (!success)
            {
                return NotFound("Funcionário não encontrado.");
            }

           
            return NoContent();

        }

    }
    }


