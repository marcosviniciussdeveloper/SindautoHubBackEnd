
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualBasic;
using SendGrid.Helpers.Errors.Model;
using SindautoHub.Application.Common.Mappings;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Application.Service
{
    /// <summary>
    /// Classe que se comunica com o repositório de Funcionarios para realizar operações de CRUD.
    /// sumary>
    public class FuncionarioService : IFuncionarioServices
    {
        private readonly IUsersRespository _funcionarioRespository;
        private readonly ICargoRepository _cargoRepository;
        private readonly ISetoresRepository _setoresRepository;
        private readonly IMapper _mapper;
        private readonly IunitOfwork _unitOfWork;

        public FuncionarioService(ISetoresRepository setoresRepository, ICargoRepository cargoRepository, IunitOfwork iunit0Fwork, IMapper mapper, IUsersRespository funcionarioRespository)
        {
            _setoresRepository = setoresRepository;
            _unitOfWork = iunit0Fwork;
            _cargoRepository = cargoRepository;
            _mapper = mapper;
            _funcionarioRespository = funcionarioRespository;
        }

        public async Task<FuncionarioResponseDto> CreateAsync(CreateFuncionarioRequest createRequest)
        {


            var FuncionarioExistente = await _funcionarioRespository.GetByCpfAsync(createRequest.Cpf);
            if (FuncionarioExistente != null)
            {
                throw new Exception("Funcionario com esse CPF já existe.");
            }

            var verificarEmailExistente = await _funcionarioRespository.GetByEmailAsync(createRequest.Email);
            if (verificarEmailExistente != null)
            {
                throw new Exception("Funcionario com esse Email já existe.");
            }

            var funcionarioProfile = _mapper.Map<User>(createRequest);

            await _funcionarioRespository.CreateAsync(funcionarioProfile);
            await _unitOfWork.SaveChangesAsync();

            var newFuncionarioComDados = await _funcionarioRespository.GetByIdWithincludesAsync(funcionarioProfile.Id);


            return _mapper.Map<FuncionarioResponseDto>(funcionarioProfile);

        }

        public async Task<bool> DeleteAsync(Guid FuncionarioId)
        {
            var ExitingProfile = await _funcionarioRespository.GetByIdAsync(FuncionarioId);
            if (ExitingProfile == null)
            {
                throw new Exception("Funcionario não encontrado.");
            }

            await _funcionarioRespository.DeleteAsync(FuncionarioId);
            await _unitOfWork.SaveChangesAsync();

            return true;

        }

        public async Task<IEnumerable<User>> GetAllAsync(Guid FuncionarioId)
        {
            var funcionarios = _funcionarioRespository.GetAllAsync(FuncionarioId);
            if (funcionarios == null)
            {
                throw new Exception("Nenhum funcionario encontrado.");
            }

            await _unitOfWork.SaveChangesAsync();
            return await funcionarios;

        }

        public async Task<FuncionarioResponseDto?> GetByIdAsync(Guid FuncionarioId)
        {
            var funcionario = await _funcionarioRespository.GetByIdAsync(FuncionarioId);
            if (funcionario == null)
            {
                throw new Exception("Funcionario não encontrado.");
            }

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<FuncionarioResponseDto>(funcionario);
        }

        public async Task<bool> UpdateAsync(Guid funcionarioId, UpdateFuncionarioRequest updateRequest)
        {
            var funcionarioEntity = await _funcionarioRespository.GetByIdAsync(funcionarioId);

            if (funcionarioEntity is null)
                throw new NotFoundException("Funcionário não encontrado.");

            if (updateRequest.CargoId.HasValue && updateRequest.CargoId.Value != Guid.Empty)
            {
                var cargoExiste = await _cargoRepository.GetByIdAsync(updateRequest.CargoId.Value);
                if (cargoExiste is null)
                    throw new NotFoundException("O Cargo informado não existe.");

                funcionarioEntity.CargoId = updateRequest.CargoId.Value;
            }

            _mapper.Map(updateRequest, funcionarioEntity);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }


    }
}
