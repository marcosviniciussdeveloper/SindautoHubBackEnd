using AutoMapper;
using SindautoHub.Application.Dtos.TicketMessageDtos;
using SindautoHub.Domain.Entities;

public class TicketMessageProfile : Profile
{
    public TicketMessageProfile()
    {
        CreateMap<TicketMessage, TicketMessageResponse>()
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.Name));
    }
}
