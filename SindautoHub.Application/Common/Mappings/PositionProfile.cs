using AutoMapper;
using SindautoHub.Application.Dtos.PositionDtos;
using SindautoHub.Domain.Entities.Models;

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateMap<CreatePositionRequest, Position>();
        CreateMap<UpdatePositionRequest, Position>();
     

        CreateMap<Position, PositionResponse>()
            .ForMember(d => d.DescriptionDuties,
                       o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.DescriptionDuties)
                            ? "Descrição não disponível"
                            : s.DescriptionDuties));
    }
}
