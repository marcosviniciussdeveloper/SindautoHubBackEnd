using AutoMapper;
using SindautoHub.Application.Dtos.PositionDtos;
using SindautoHub.Domain.Entities.Models;

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateMap<CreatePositionRequest,Position >();
        CreateMap<UpdatePositionRequest, Position>();
        CreateMap<Position, PositionResponse>();

        CreateMap<Position, PositionResponse>()
    .ForMember(dest => dest.DescriptionDuties, opt => opt.MapFrom(src => src.DescriptionDuties ?? "Descrição não disponível"));

    }

}