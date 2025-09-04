
using Microsoft.AspNetCore.Mvc;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;



namespace SindautoHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {


        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        { 

            var loginResponse = await _authService.LoginAsync(request);

            if (loginResponse == null)
            {
                return Unauthorized(new { message = "CPF inválido ou funcionário não encontrado." });
            
            }

            return Ok(loginResponse);

        }


    }
}
