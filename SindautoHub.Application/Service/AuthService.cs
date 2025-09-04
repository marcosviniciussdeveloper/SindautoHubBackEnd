using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IunitOfwork _unitOfWork;
        private readonly IFuncionarioRespository _funcionarioRespository;
        private readonly ITokenService _tokenService;


        public AuthService(IunitOfwork unitOfWork, IFuncionarioRespository funcionarioRespository, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _funcionarioRespository = funcionarioRespository;
            _tokenService = tokenService;
        }
        public Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var funcionario = _funcionarioRespository.GetByCpfAsync(loginRequest.Cpf).Result;
            if (funcionario == null)
            {
               throw new Exception("Cpf incorreto ou funcionario não existe");
            }

            var token = _tokenService.GenerateToken(funcionario);

            return Task.FromResult(new LoginResponse { Token = token });

        }
    }
}
