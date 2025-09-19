using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Application.Dtos
{
    public class LoginRequest
    {

        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginRequest() { }


        public LoginRequest(string username, string password)
        {
            UserName = username;
            Password = password;
        }

    }
}
    public class LoginResponse
    {
    public string Token { get; set; }

    }

   

