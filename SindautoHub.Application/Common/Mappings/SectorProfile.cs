using AutoMapper;
using SindautoHub.Application.Dtos.SectorDtos;
using SindautoHub.Domain.Entities.Models;

public class SectorProfile : Profile
{
    public SectorProfile()
    {
        CreateMap<CreateSectorRequest, Sector>();
        CreateMap<UpdateSectorRequest, Sector>();

        // Se os nomes batem (NameSector, Description, OpeningsHours), não precisa custom map
        CreateMap<Sector, SectorResponse>()
            .ForMember(d => d.MembersCount,
                       o => o.MapFrom(s => s.Users == null ? 0 : s.Users.Count));
    }
}