using System.Text.Json;
using AutoMapper;
using SindautoHub.Application.Dtos.SectorDtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interfaces;

namespace SindautoHub.Application.Service;

public class SectorService : ISectorService
{
    private readonly ISectorRepository _sectorRepository;
    private readonly IMapper _mapper;
    private readonly IunitOfwork _unitOfWork;
    private readonly ICacheService _cache;

    private const string CacheKey = "sectors_all";

    public SectorService(
        ISectorRepository sectorRepository,
        IMapper mapper,
        IunitOfwork unitOfWork,
        ICacheService cache)
    {
        _sectorRepository = sectorRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<SectorResponse> CreateAsync(CreateSectorRequest request)
    {
        var entity = _mapper.Map<Sector>(request);
        await _sectorRepository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        // invalida cache
        await _cache.RemoveAsync(CacheKey);

        return _mapper.Map<SectorResponse>(entity);
    }

    public async Task<List<SectorResponse>> GetAllAsync()
    {
        var cached = await _cache.GetAsync(CacheKey);
        if (!string.IsNullOrEmpty(cached))
        {
            return JsonSerializer.Deserialize<List<SectorResponse>>(cached)
                   ?? new List<SectorResponse>();
        }

        var sectors = await _sectorRepository.GetAllAsync();
        var response = _mapper.Map<List<SectorResponse>>(sectors);

        var json = JsonSerializer.Serialize(response);
        await _cache.SetAsync(CacheKey, json, TimeSpan.FromMinutes(5));

        return response;
    }

    public async Task<SectorResponse> GetByIdAsync(Guid id)
    {
        var sector = await _sectorRepository.GetByIdAsync(id);
        if (sector == null) return null!;
        return _mapper.Map<SectorResponse>(sector);
    }

    public async Task<SectorResponse> UpdateAsync(Guid id, UpdateSectorRequest request)
    {
        var sector = await _sectorRepository.GetByIdAsync(id);
        if (sector == null) return null!;

        if (!string.IsNullOrWhiteSpace(request.Name))
            sector.NameSector = request.Name;

        if (!string.IsNullOrWhiteSpace(request.Description))
            sector.Description = request.Description;

        if (!string.IsNullOrWhiteSpace(request.OpeningsHours))
            sector.OpeningsHours = request.OpeningsHours;

        await _sectorRepository.UpdateAsync(sector);
        await _unitOfWork.SaveChangesAsync();

        await _cache.RemoveAsync(CacheKey);

        return _mapper.Map<SectorResponse>(sector);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sector = await _sectorRepository.GetByIdAsync(id);
        if (sector == null) return false;

        await _sectorRepository.DeleteAsync(sector);
        await _unitOfWork.SaveChangesAsync();

        // invalida cache
        await _cache.RemoveAsync(CacheKey);

        return true;
    }
}
