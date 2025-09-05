using AutoMapper;
using SindautoHub.Application.Dtos; 
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Application.Common.Mappings;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<CreateFuncionarioRequest, User>();


        CreateMap<UpdateFuncionarioRequest, User>()
     .ForMember(dest => dest.CargoId, opt => opt.Ignore()) // IGNORA O MAPEAMENTO AUTOMÁTICO DE CargoId
     .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<User, FuncionarioResponseDto>() 
            .ForMember(
                dest => dest.NomeDoCargo,
                opt => opt.MapFrom(src => src.cargo.Nome) 
            )
            .ForMember(
                dest => dest.NomeDoSetor,
                opt => opt.MapFrom(src => src.setor.NomeSetor) 
            )
            .ForMember(
                dest => dest.TipoContratacao,
                opt => opt.MapFrom(src => src.TipoContratacao.ToString())
            );
    }
}