using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Features;
using SindautoHub.Application.Common.Mappings;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Application.Service
{
    public class CargoServices : ICargoServices
    {
        private readonly ICargoRepository _cargoRepository;
        private readonly IUsersRespository _funcionarioRespository;
        private readonly IunitOfwork _unitOfWork;
        private readonly IMapper _mapper;
        public CargoServices(IUsersRespository funcionario, IunitOfwork iunitOfwork, IMapper mapper, ICargoRepository cargoRepository)
        {
            _funcionarioRespository = funcionario;
            _unitOfWork = iunitOfwork;
            _mapper = mapper;
            _cargoRepository = cargoRepository;
        }

        public async Task<Cargo> CreateAsync(CreateCargoRequest CreateRequest)
        {
            var cargoExistente = await _cargoRepository.GetByNameAsync(CreateRequest.Nome);
            if (cargoExistente != null)
            {
                throw new Exception("Cargo já existe.");
            }

            var cargoProfile = _mapper.Map<Cargo>(CreateRequest);

            await _cargoRepository.CreateAsync(cargoProfile);
            await _unitOfWork.SaveChangesAsync();

            return cargoProfile;
        }

        public async Task<bool> DeleteAsync(Guid cargoId)
        {
            var existingCargo = await _cargoRepository.GetByIdAsync(cargoId);
            if (existingCargo == null)
            {
                throw new Exception("Cargo não encontrado.");   
            }

            var funncionariosnocargo = await _funcionarioRespository.GetByIdAsync(cargoId);

            if (funncionariosnocargo != null)
            {
                throw new Exception("Não é possível deletar o cargo, pois existem funcionários associados a ele.");
            }
            
                await _cargoRepository.DeleteAsync(cargoId);
                await _unitOfWork.SaveChangesAsync();
                return true;
            
        }

        public async Task<IEnumerable<Cargo>> GetAllAsync(Guid cargoId)
        {
            var cargos = _cargoRepository.GetAllAsync(cargoId);
            if (cargos == null)
            {
                throw new Exception("Nenhum cargo encontrado.");
            }

            await _unitOfWork.SaveChangesAsync();
            return await cargos;
        }

        public async Task<CargoResponse?> GetByIdAsync(Guid cargoId)
        {
            var cargo = _cargoRepository.GetByIdAsync(cargoId);
            if (cargo == null)
            {
                throw new Exception("Cargo não encontrado.");

            }

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CargoResponse>(cargo);
        }

        public async Task<bool> UpdateAsync(Guid cargoId, UpdateCargoRequest updateRequest)
        {
            var cargoExistente = await _cargoRepository.GetByIdAsync(cargoId);
            if (cargoExistente == null) return false;


            _mapper.Map(updateRequest, cargoExistente);

            await _cargoRepository.UpdateAsync(cargoId);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
