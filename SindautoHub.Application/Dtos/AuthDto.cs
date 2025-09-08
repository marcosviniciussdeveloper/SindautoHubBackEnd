using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Application.Dtos
{
    public class LoginRequest
    {

        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
    public class LoginResponse
    {
    public string Token { get; set; }

    public List<string> Permissions { get; set; }
    public AuthuserDTO user { get; set; }

    }

    public class AuthuserDTO
    {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    }


