using System.Text.Json;
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
        private readonly ICacheService _cache;
        private static readonly JsonSerializerOptions JsonOpts = new(JsonSerializerDefaults.Web);
        private const string UserListCacheKey = "users_all";

        public UserController(ICacheService cache, IUserServices userService)
        {
            _userService = userService;
            _cache = cache;
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var user = await _userService.CreateAsync(request);

            await _cache.RemoveAsync(UserListCacheKey);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new
            {
                message = "Usuário criado com sucesso!",
                data = user
            });
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cached = await _cache.GetAsync(UserListCacheKey);
            if (!string.IsNullOrWhiteSpace(cached))
            {
                var usersFromCache = JsonSerializer.Deserialize<List<UserResponse>>(cached!, JsonOpts)
                                     ?? new List<UserResponse>();

                return Ok(new
                {
                    message = $"Total de usuários encontrados (cache): {usersFromCache.Count}",
                    data = usersFromCache
                });
            }

            var users = (await _userService.GetAllAsync()).ToList();
            var json = JsonSerializer.Serialize(users, JsonOpts);

            await _cache.SetAsync(UserListCacheKey, json);

            return Ok(new
            {
                message = $"Total de usuários encontrados (db): {users.Count}",
                data = users
            });
        }


        [HttpGet("sector/{sectorId}")]
        public async Task<IActionResult> GetUsersBySector(Guid sectorId)
        {
            var result = await _userService.GetUsersBySectorAsync(sectorId);

            return Ok(new
            {
                message = $"Total de usuários encontrados no setor {sectorId}: {result.Count}",
                data = result
            });
        }



        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(user);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
        {
            var updatedUser = await _userService.UpdateAsync(id, request);

            await _cache.RemoveAsync(UserListCacheKey);

            return Ok(new
            {
                message = "Usuário atualizado com sucesso!",
                data = updatedUser
            });
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);

            await _cache.RemoveAsync(UserListCacheKey);

            return Ok(new
            {
                message = "Usuário excluído com sucesso."
            });
        }
    }
}
