using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Enums;
using SindautoHub.Domain.Interfaces;

public class AuthService : IAuthService
{

    private readonly IUserRepository _userRepository;
    private readonly IunitOfwork _unitOfWork;
    private readonly ITokenService _tokenService;

    public AuthService(ITokenService tokenService, IUserRepository userRepository, IunitOfwork iunitOfwork)
    {
        _tokenService = tokenService;
        _unitOfWork = iunitOfwork;
        _userRepository = userRepository;

    }

    private string HashPassword(string password)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByNameAsync(request.UserName);
        if (user == null)
        {
            throw new Exception("Usuario não encontrado");
        }

        var inputPasswordHash = HashPassword(request.Password);
        if (inputPasswordHash == null)
        {
            throw new Exception("Senha incorreta");
        }


        var permissions = RolePermissions.GetPermissions(user.Role)
            .Select(x => x.ToString())
            .ToList();

        var token = _tokenService.GenerateToken(user);

        return new LoginResponse
        {
            Token = token,
            Permissions = permissions

        };




    }
}
