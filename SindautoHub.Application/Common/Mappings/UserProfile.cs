using AutoMapper;
using SindautoHub.Application.Dtos.ChatDtos;
using SindautoHub.Application.Dtos.UserDtos;
using SindautoHub.Domain.Entities;

public class UserProfile : Profile
{
    private const string StorageBaseUrl = "https://xitvsgswawdtiynzcupb.supabase.co/storage/v1/object/public/user-photos/";

    public UserProfile()
    {
        // Criação de usuário
        CreateMap<CreateUserRequest, User>();

        // Atualização de usuário (ignora nulls)
        CreateMap<UpdateUserRequest, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Autenticação
        CreateMap<User, AuthuserDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.SectorId, opt => opt.MapFrom(src => src.SectorId))
            .ForMember(dest => dest.SectorName, opt => opt.MapFrom(src => src.Sector != null ? src.Sector.NameSector : null))
            .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position != null ? src.Position.PositionName : null))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.PhotoPath) ? null : $"{StorageBaseUrl}{src.PhotoPath}"
            ));

        // Resposta completa de usuário
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.SectorId, opt => opt.MapFrom(src => src.SectorId))
            .ForMember(dest => dest.SectorName, opt => opt.MapFrom(src => src.Sector != null ? src.Sector.NameSector : null))
            .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position != null ? src.Position.PositionName : null))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.PhotoPath) ? null : $"{StorageBaseUrl}{src.PhotoPath}"
            ));

    }
}
