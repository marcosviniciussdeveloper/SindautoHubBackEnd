using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Enums;
using SindautoHub.Domain.Interfaces;
using System.Security.Authentication;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IunitOfwork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
    private IUserRepository object1;
    private IPasswordHasher passwordHasher;
    private ITokenService object2;

    public AuthService(IUserRepository object1, IPasswordHasher passwordHasher, ITokenService object2)
    {
        this.object1 = object1;
        this.passwordHasher = passwordHasher;
        this.object2 = object2;
    }

    public AuthService(
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IUserRepository userRepository,
        IunitOfwork iunitOfwork)
    {
        _tokenService = tokenService;
        _unitOfWork = iunitOfwork;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByNameAsync(request.UserName);
        if (user == null)
            throw new UnauthorizedAccessException("Usuário não encontrado.");

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
            throw new UnauthorizedAccessException("Senha incorreta.");

        var permissions = RolePermissions.GetPermissions(user.Role)
            .Select(p => p.ToString())
            .ToList();

        var token = _tokenService.GenerateToken(user);

        return new LoginResponse
        {
            Token = token,
            Permissions = permissions,
            user = new AuthuserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role
            }
        };
    }
}
