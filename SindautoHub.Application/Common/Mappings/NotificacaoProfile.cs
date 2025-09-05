using System;
using AutoMapper;
using SindautoHub.Application.Dtos;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Application.Common.Mappings;

public class NotificacaoProfile : Profile
{
    public NotificacaoProfile()
    {
        
        CreateMap<CreateNotificacaoRequest, announcements>()  
            .ForMember(dest => dest.Lida, opt => opt.MapFrom(src => false)) 
            .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow)); 


       
        CreateMap<announcements, NotificacaoResponse>();

    }
}