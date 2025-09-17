using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Enums;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interfaces;
using System.Security.Authentication;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IunitOfwork _unitOfWork;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IunitOfwork unitOfWork)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByNameAsync(request.UserName);
        if (user == null)
            throw new UnauthorizedAccessException("Usuário não encontrado.");

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
            throw new UnauthorizedAccessException("Senha incorreta.");

        var permissions = (RolePermissions.GetPermissions(user.Role) ?? Enumerable.Empty<Permission>())
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
                SectorId = user.SectorId,
                UserName = user.UserName, 
                SectorName = user.Sector != null ? user.Sector.NameSector : string.Empty,
                PositionName = user.Position != null ? user.Position.PositionName : string.Empty,
                Name = user.Name,
                 Email = user.Email,
                Role = user.Role
            }
        };
    }
}
