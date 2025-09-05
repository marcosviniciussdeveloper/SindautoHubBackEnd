using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IunitOfwork _unitOfWork;
        private readonly IUsersRespository _funcionarioRespository;
        private readonly ITokenService _tokenService;


        public AuthService(IunitOfwork unitOfWork, IUsersRespository funcionarioRespository, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _funcionarioRespository = funcionarioRespository;
            _tokenService = tokenService;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {

            var cpf = Regex.Replace(loginRequest.Cpf ?? string.Empty, @"\D", "");

            if (cpf.Length != 11)
            {
                throw new Exception("Cpf deve ter 11 dígitos");
            }

            var funcionario = await _funcionarioRespository.GetByCpfAsync(cpf);
            if (funcionario is null)
                throw new Exception("Cpf incorreto ou funcionario não existe");

            var token = _tokenService.GenerateToken(funcionario);

            return new LoginResponse { Token = token };
        }
    }
}
