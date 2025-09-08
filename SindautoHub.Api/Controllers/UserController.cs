using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SindautoHub.Application.Dtos.UserDtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;

        public UserController(IUserServices userService)
        {
            _userService = userService;
        }

        // POST: api/User
        [HttpPost]
        //[Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var user = await _userService.CreateAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new
            {
                message = "Usuário criado com sucesso!",
                data = user
            });
        }

        // GET: api/User
        [HttpGet]
    //    [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Ok(new
            {
                message = $"Total de usuários encontrados: {users.Count()}",
                data = users
            });
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
     //   [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(user);
            
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
        {
            var updatedUser = await _userService.UpdateAsync(id, request);

            return Ok(new
            {
                message = "Usuário atualizado com sucesso!",
                data = updatedUser
            });
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);

            return Ok(new
            {
                message = "Usuário excluído com sucesso."
            });
        }
    }
}
