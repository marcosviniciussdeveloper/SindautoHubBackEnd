using AutoMapper;
using SindautoHub.Application.Dtos.ChatDtos;
using SindautoHub.Application.Dtos.UserDtos;
using SindautoHub.Domain.Entities;

namespace SindautoHub.Application.Common.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {

        CreateMap<CreateUserRequest, User>();
        CreateMap<UpdateUserRequest, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<User, AuthuserDTO>()

            .ForMember(dest => dest.SectorId, opt => opt.MapFrom(src => src.SectorId))
            .ForMember(dest => dest.SectorName, opt => opt.MapFrom(src => src.Sector != null ? src.Sector.NameSector : null));


        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position != null ? src.Position.Name : null))
            .ForMember(dest => dest.SectorName, opt => opt.MapFrom(src => src.Sector != null ? src.Sector.NameSector : null));
            
        CreateMap<User, UserSummaryResponse>();
    }
}