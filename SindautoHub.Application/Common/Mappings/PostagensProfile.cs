using System;
using AutoMapper;
using SindautoHub.Application.Dtos;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Application.Common.Mappings;

public class PostagemProfile : Profile
{
    public PostagemProfile()
    {
       
        CreateMap<CreatePostagemRequest, Postagem>()
            
            .ForMember(dest => dest.DataPublicacao, opt => opt.MapFrom(src => DateTime.UtcNow));
    

        CreateMap<UpdatePostagemRequest, Postagem>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


        CreateMap<Postagem, PostagemResponse>()
            .ForMember(
                dest => dest.AutorNome,
                opt => opt.MapFrom(src => src.Autor.Nome) 
            )
            .ForMember(
                dest => dest.AutorFotoUrl,
                opt => opt.MapFrom(src => src.Autor.FotoUrl) 
            );
    }
}