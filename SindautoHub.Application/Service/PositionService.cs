using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SindautoHub.Application.Dtos.PositionDtos;
using SindautoHub.Application.Dtos.SectorDtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities.Models;
using System.Text.Json;

public class PositionServices : IPositionServices
{
    private readonly IPositionRepository _positionRepository;
    private readonly IunitOfwork _unitOfwork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;

    public const string CacheKey = "Position_All";

    public PositionServices (
        ICacheService cache ,
        IunitOfwork unitOfwork,
        IMapper mapper,
        IPositionRepository positionRepository  
        )
    {
        _cache = cache;
        _unitOfwork = unitOfwork;
        _mapper = mapper;
        _positionRepository = positionRepository;

    }
    public async Task<PositionResponse> CreateAsync(CreatePositionRequest request)
    {
       var entity = _mapper.Map<Position>(request);
        await _positionRepository.CreateAsync(entity);
        await _unitOfwork.SaveChangesAsync();

        await _cache.RemoveAsync(CacheKey);

        return _mapper.Map<PositionResponse>(entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var position = await _positionRepository.GetByIdAsync(id);
        if (position == null)  return false;

        await _positionRepository.DeleteAsync(position);
        await _unitOfwork.SaveChangesAsync();

        await _cache.RemoveAsync(CacheKey);

        return true;
    }

    public async Task<List<PositionResponse>> GetAllAsync()
    {
        var cached = await _cache.GetAsync(CacheKey);
        if (!string.IsNullOrEmpty(cached))
        {
            return JsonSerializer.Deserialize<List<PositionResponse>>(cached)
              ?? new List<PositionResponse>();
        }

        var position = await _positionRepository.GetAllAsync();
        var response = _mapper.Map<List<PositionResponse>>(position);

        var json = JsonSerializer.Serialize(response);
        await _cache.SetAsync(CacheKey, json, TimeSpan.FromMinutes(5));

        return response;
    }

    public async Task<PositionResponse> GetByIdAsync(Guid id)
    {
        var position = await _positionRepository.GetByIdAsync(id);
        if (position == null) return null;
        return _mapper.Map<PositionResponse>(position);

    }

    public async Task<PositionResponse> UpdateAsync(Guid id, UpdatePositionRequest request)
    {
        var position = await _positionRepository.GetByIdAsync(id);
        if (position == null) return null;

        if (!string.IsNullOrWhiteSpace(request.PositionName))
         position.PositionName = request.PositionName;

        if (!string.IsNullOrWhiteSpace(request.DescriptionDuties))
           position.DescriptionDuties = request.DescriptionDuties;

        await _positionRepository.UpdateAsync(position);
        await _unitOfwork.SaveChangesAsync();
       
        await _cache.RemoveAsync(CacheKey);

        return _mapper.Map<PositionResponse>(position);
    }
}
