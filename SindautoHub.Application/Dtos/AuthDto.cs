using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Application.Dtos
{
    public class LoginRequest
    {
        public string Cpf { get; set; }
      
    }
}
    public class LoginResponse
    {
    public string Token { get; set; }
   
    }