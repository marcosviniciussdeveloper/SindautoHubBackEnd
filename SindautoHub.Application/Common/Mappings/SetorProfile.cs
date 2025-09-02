using AutoMapper;
using SindautoHub.Application.Dtos;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Application.Common.Mappings;

public class SetorProfile : Profile
{
    public SetorProfile()
    {
      
        CreateMap<CreateSetorRequest, Setor>();

        
        CreateMap<UpdateSetorRequest, Setor>();

        CreateMap<Setor, SetorResponse>()
            .ForMember(dest => dest.TotalFuncionarios,
                       opt => opt.MapFrom(src => src.Funcionarios.Count)); 
    }
}