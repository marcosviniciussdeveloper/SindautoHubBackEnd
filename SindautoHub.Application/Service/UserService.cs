using AutoMapper;
using SindautoHub.Application.Dtos.UserDtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Enums;
using SindautoHub.Domain.Interface;
using SindautoHub.Domain.Interfaces;
using StackExchange.Redis;

public class UserService : IUserServices
{
    private readonly IUserRepository _useRepository;
    private readonly IunitOfwork _iunitOfwork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ICacheService _cache;
    private static readonly object builder;

    public UserService(
        IUserRepository useRepository,
        IunitOfwork iunitOfwork,
        IMapper mapper,
        IPasswordHasher passwordHasher,
        ICacheService cache)
    {
        _useRepository = useRepository;
        _iunitOfwork = iunitOfwork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _cache = cache;
    }

    public async Task<UserResponse> CreateAsync(CreateUserRequest request)
    {
        string cleanCpf = new string(request.Cpf.Where(char.IsDigit).ToArray());

        var existingUser = await _useRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            throw new BadRequestException("Já existe um usuário cadastrado com este e-mail.");

        var existingCpf = await _useRepository.GetByCpfAsync(cleanCpf);
        if (existingCpf != null)
            throw new BadRequestException("Já existe um usuário cadastrado com este CPF.");

        var newUser = _mapper.Map<User>(request);
        newUser.Cpf = cleanCpf;
        newUser.Password = _passwordHasher.HashPassword(request.Password);
        newUser.CreatedAt = DateTime.UtcNow;
        newUser.UpdatedAt = DateTime.UtcNow;

        await _useRepository.CreateAsync(newUser);
        await _iunitOfwork.SaveChangesAsync();

        string? positionName = null;
        string? sectorName = null;

        if (newUser.PositionId.HasValue)
        {
            var position = await _iunitOfwork.PositionRepository.GetByIdAsync(newUser.PositionId.Value);
            positionName = position?.Name;
        }

        if (newUser.SectorId.HasValue)
        {
            var sector = await _iunitOfwork.SectorRepository.GetByIdAsync(newUser.SectorId.Value);
            sectorName = sector?.NameSector;
        }

        var response = _mapper.Map<UserResponse>(newUser);
        response.PositionName = positionName ?? string.Empty;
        response.SectorName = sectorName ?? string.Empty;

        return response;
    }

    public async Task<UserResponse> GetByIdAsync(Guid id)
    {
       
        var user = await _useRepository.GetByIdWithDetailsAsync(id);
        if (user == null)
            throw new Exception("Usuário não encontrado.");

       
        var response = _mapper.Map<UserResponse>(user);


        return response;
    }

    public async Task<List<UserBySectorResponse>> GetUsersBySectorAsync(Guid sectorId)
    {
        var users = await _useRepository.GetBySectorIdWithDetailsAsync(sectorId);

        return users.Select(u => new UserBySectorResponse
        {
            Id = u.Id,
            Name = u.Name,
            Position = u.Position?.Name ?? "Sem cargo",
            IsOnline = u.PresenceStatus == PresenceStatus.Online
        }).ToList();
    }



    public async Task<List<UserResponse>> GetAllAsync()
    {
        var users = await _useRepository.GetAllAsync();
        return users.Select(u => new UserResponse
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Role = u.Role,
            Status = u.Status,
            CreatedAt = u.CreatedAt,
            PositionName = u.Position.Name,
            SectorName = u.Sector.NameSector
        }).ToList();

    }

    public async Task<UserResponse> UpdateAsync(Guid id, UpdateUserRequest request)
    {
        var user = await _useRepository.GetByIdAsync(id);
        if (user == null)
            throw new Exception("Usuário não encontrado.");

        if (!string.IsNullOrWhiteSpace(request.Name))
            user.Name = request.Name;

        if (!string.IsNullOrWhiteSpace(request.UserName))
            user.UserName = request.UserName;

        if (!string.IsNullOrWhiteSpace(request.Email))
            user.Email = request.Email;

        if (!string.IsNullOrWhiteSpace(request.Role))
            user.Role = request.Role;

        if (!string.IsNullOrWhiteSpace(request.Password))
            user.Password = _passwordHasher.HashPassword(request.Password);

        user.UpdatedAt = DateTime.UtcNow;

        await _useRepository.UpdateAsync(user);
        await _iunitOfwork.SaveChangesAsync();

        return _mapper.Map<UserResponse>(user);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _useRepository.GetByIdAsync(id);
        if (user == null)
            throw new Exception("Usuário não encontrado.");

        await _useRepository.DeleteAsync(user);
        await _iunitOfwork.SaveChangesAsync();
        return true;
    }
}
