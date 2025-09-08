using Microsoft.AspNetCore.Mvc;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserServices _userServices;


        public AuthController(IUserServices userServices, IAuthService authService)
        {
            _authService = authService;
            _userServices = userServices;
        }

        [HttpPost("login")]
        
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
            var result = await _authService.LoginAsync(request);
            return Ok(result);

        }
    }
}
