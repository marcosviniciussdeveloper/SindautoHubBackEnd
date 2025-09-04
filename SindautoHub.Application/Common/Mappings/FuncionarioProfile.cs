using AutoMapper;
using SindautoHub.Application.Dtos; // Ou o namespace dos seus DTOs
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Models; // Ou o namespace das suas Entidades

namespace SindautoHub.Application.Common.Mappings;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        // Mapa de ENTRADA: De DTO para Entidade (Para Criar um novo Funcionário)
        CreateMap<CreateFuncionarioRequest, Funcionario>();

        // Mapa de ATUALIZAÇÃO: De DTO para Entidade
        CreateMap<UpdateFuncionarioRequest, Funcionario>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Mapa de SAÍDA: De Entidade para DTO (Para enviar ao Front-end)
        CreateMap<Funcionario, FuncionarioResponseDto>() // Usando o DTO de resposta correto
            .ForMember(
                dest => dest.NomeDoCargo,
                opt => opt.MapFrom(src => src.cargo.Nome) // Correção: 'C' maiúsculo
            )
            .ForMember(
                dest => dest.NomeDoSetor,
                opt => opt.MapFrom(src => src.setor.NomeSetor) // Correção: 'S' maiúsculo e a propriedade 'Nome'
            )
            .ForMember(
                dest => dest.TipoContratacao,
                opt => opt.MapFrom(src => src.TipoContratacao.ToString())
            );
    }
}