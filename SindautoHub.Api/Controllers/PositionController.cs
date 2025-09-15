using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using SindautoHub.Application.Dtos.PositionDtos;
using SindautoHub.Application.Interface;
using SindautoHub.Application.Service;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]


    public class PositionController : ControllerBase
    {

        private readonly IPositionServices _positionServices;

        public PositionController(IPositionServices positionServices)
        {

            _positionServices = positionServices;

        }

        [HttpPost]
        public async Task<IActionResult> Create ([FromBody]  CreatePositionRequest  request)
        {
            var created = await _positionServices.CreateAsync(request);
            return CreatedAtAction (nameof(GetbyId ) , new {id = created.Id}, new
            {  
                
                message = "Cargo Criado com sucesso", 
                data = created
            });
        }
      
        [HttpGet("id")]

        public async Task<IActionResult> GetbyId(Guid id)
        {

            var position = await _positionServices.GetByIdAsync(id);
            if (position == null)
                return NotFound(new { message = "Cargo não encontrado" });
            return Ok(position);

        }
         //PUT : api/Position
        [HttpPut]
       
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePositionRequest request)
        {
            var update = await _positionServices.UpdateAsync(id, request);
            if (update == null)
                return NotFound(new { Message = "Cargo não encontrado para atualização" });

            return Ok(new
            {
                messsage = "Cargo Atualizado Com Sucesso",
                data = update


            });


        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
          
        var result = await _positionServices.GetAllAsync();
        return Ok(new { message = "ok", data = result });

        }

        //DELETE : api/Position
        [HttpDelete]
        public async Task <IActionResult> Delete(Guid id)
        {
            await _positionServices.DeleteAsync(id);
            
            return Ok (new
            {
                
                message = "Usuario Deletado com sucesso" 
                   
            });
        }

      }
    }
