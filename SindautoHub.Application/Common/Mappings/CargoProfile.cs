using AutoMapper;
using SindautoHub.Application.Dtos;

using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Application.Common.Mappings;

public class CargoProfile : Profile
{
    public CargoProfile()
    {
     
        CreateMap<CreateCargoRequest, Cargo>();

        CreateMap<UpdateCargoRequest, Cargo>();


        CreateMap<Cargo, CargoResponse>();
    }
}