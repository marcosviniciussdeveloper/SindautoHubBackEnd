
using AutoMapper;
using SindautoHub.Application.Dtos.SectorDtos;
using SindautoHub.Domain.Entities.Models;

public class SectorProfile : Profile
{
    public SectorProfile()
    {
        CreateMap<CreateSectorRequest, Sector>();
        CreateMap<UpdateSectorRequest, Sector>();
        CreateMap<Sector, SectorResponse>()
            .ForMember(dest => dest.MembersCount, opt => opt.MapFrom(src => src.Users.Count)); // assumindo rel.
    }
}