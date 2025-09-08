using AutoMapper;
using SindautoHub.Application.Dtos.ChatDtos;
using SindautoHub.Domain.Entities;

public class ChatProfile : Profile
{
    public ChatProfile()
    {
        CreateMap<CreateChatRequest, Chat>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<Chat, ChatResponse>()
            .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => 
                src.ChatUsers.Select(cu => new UserSummaryResponse
                {
                    Id = cu.User.Id,
                    Name = cu.User.Name
                })))
            .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));
    }
}
