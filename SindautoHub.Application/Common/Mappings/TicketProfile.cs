using AutoMapper;
using SindautoHub.Application.Dtos.TicketDtos;
using SindautoHub.Domain.Entities;

public class TicketProfile : Profile
{
    public TicketProfile()
    {
        CreateMap<CreateTicketRequest, Ticket>()
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Open"))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<Ticket, TicketResponse>()
            .ForMember(dest => dest.ClienteName, opt => opt.MapFrom(src => src.Cliente.Name))
            .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.Agent != null ? src.Agent.Name : null))
            .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Cliente.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Cliente.UserName))
            .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.AgenteId));
    }
}
