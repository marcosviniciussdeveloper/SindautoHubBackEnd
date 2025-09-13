using System.Text.Json;
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

    // Base pública do Supabase Storage (ajuste se o bucket tiver outro nome)
    private const string StorageBaseUrl =
        "https://xitvsgswawdtiynzcupb.supabase.co/storage/v1/object/public/avatars/";

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

        // salva PhotoPath se enviado no request
        if (!string.IsNullOrWhiteSpace(request.PhotoPath))
            newUser.PhotoPath = request.PhotoPath;

        await _useRepository.CreateAsync(newUser);
        await _iunitOfwork.SaveChangesAsync();

        await _cache.RemoveAsync("user.list");

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
        response.PhotoUrl = string.IsNullOrEmpty(newUser.PhotoPath) ? null : $"{StorageBaseUrl}{newUser.PhotoPath}";

        return response;
    }

    public async Task<UserResponse> GetByIdAsync(Guid id)
    {
        var user = await _useRepository.GetByIdWithDetailsAsync(id);

        if (user == null || user.Status != Status.Ativo)
            throw new Exception("Usuário não encontrado ou inativo.");

        var response = _mapper.Map<UserResponse>(user);
        response.PhotoUrl = string.IsNullOrEmpty(user.PhotoPath) ? null : $"{StorageBaseUrl}{user.PhotoPath}";
        return response;
    }

    

    public async Task<List<UserBySectorResponse>> GetUsersBySectorAsync(Guid sectorId)
    {
        var users = await _useRepository.GetBySectorIdWithDetailsAsync(sectorId);

        return users.Select(u => new UserBySectorResponse
        {
            Id = u.Id,
            Name = u.Name,
            Status = u.Status,
            Position = u.Position?.Name ?? "Sem cargo",
            IsOnline = u.PresenceStatus == PresenceStatus.Online,
            PhotoUrl = string.IsNullOrEmpty(u.PhotoPath) ? null : $"{StorageBaseUrl}{u.PhotoPath}"
        }).ToList();
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
        const string cacheKey = "user.list";

        var cached = await _cache.GetAsync(cacheKey);
        if (!string.IsNullOrEmpty(cached))
        {
            return JsonSerializer.Deserialize<List<UserResponse>>(cached)
                   ?? new List<UserResponse>();
        }

        var users = await _useRepository.GetAllAsync();

        var response = users.Select(u => new UserResponse
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Role = u.Role,
            Status = u.Status,
            PositionId = u.PositionId,
            SectorId = u.SectorId,
            CreatedAt = u.CreatedAt,
            PositionName = u.Position?.Name ?? string.Empty,
            SectorName = u.Sector?.NameSector ?? string.Empty,
            PhotoUrl = string.IsNullOrEmpty(u.PhotoPath) ? null : $"{StorageBaseUrl}{u.PhotoPath}"
        }).ToList();

        var json = JsonSerializer.Serialize(response);
        await _cache.SetAsync(cacheKey, json, TimeSpan.FromMinutes(5));

        return response;
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

        if (!string.IsNullOrWhiteSpace(request.PhotoPath))
            user.PhotoPath = request.PhotoPath;

        if (request.Status.HasValue) 
            user.Status = request.Status.Value;

        user.UpdatedAt = DateTime.UtcNow;

        await _useRepository.UpdateAsync(user);
        await _iunitOfwork.SaveChangesAsync();
        await _cache.RemoveAsync("user.list");

        var response = _mapper.Map<UserResponse>(user);
        response.PhotoUrl = string.IsNullOrEmpty(user.PhotoPath) ? null : $"{StorageBaseUrl}{user.PhotoPath}";
        return response;
    }


    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _useRepository.GetByIdAsync(id);
        if (user == null)
            throw new Exception("Usuário não encontrado.");

        user.Status = Status.Inativo;
        user.UpdatedAt = DateTime.UtcNow;

        await _useRepository.UpdateAsync(user);
        await _cache.RemoveAsync("user.list");
        await _iunitOfwork.SaveChangesAsync();
        return true;
    }
}
