using AutoMapper;
using SindautoHub.Application.Dtos.AnnouncementDtos;
using SindautoHub.Domain.Entities;

public class AnnouncementProfile : Profile
{
    public AnnouncementProfile()
    {
        CreateMap<CreateAnnouncementRequest, Announcement>();

        CreateMap<UpdateAnnouncementRequest, Announcement>()
            .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null)); // bom para updates

        CreateMap<Announcement, AnnouncementResponse>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
    }
}
