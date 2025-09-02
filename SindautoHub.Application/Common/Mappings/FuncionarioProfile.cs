using System.Xml.Serialization;
using AutoMapper;
using SindautoHub.Application.Dtos;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Application.Common.Mappings;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        // Mapeamento de ENTRADA: De DTO para Entidade
        // Usado para criar um novo funcionário.
        CreateMap<CreateFuncionarioRequest, Funcionario>();

        // Mapeamento de ATUALIZAÇÃO: De DTO para Entidade
        // Usado para atualizar um funcionário existente.
        // O .ForAllMembers(opts => opts.Condition(...)) garante que apenas
        // os valores não nulos do DTO de requisição irão sobrescrever os da entidade.
        CreateMap<CreateUpdateFuncionarioRequest, Funcionario>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


    
        CreateMap<Funcionario, FuncionarioResponseDto>()
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