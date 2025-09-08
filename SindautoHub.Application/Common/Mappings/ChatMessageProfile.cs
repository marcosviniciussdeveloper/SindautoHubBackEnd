using AutoMapper;
using SindautoHub.Application.Dtos.ChatMessageDtos;
using SindautoHub.Domain.Entities;

public class ChatMessageProfile : Profile
{
    public ChatMessageProfile()
    {
        CreateMap<ChatMessage, ChatMessageResponse>()
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.Name));
    }
}
