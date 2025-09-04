
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualBasic;
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
        private readonly IFuncionarioRespository _funcionarioRespository;

        private readonly IMapper _mapper;
        private readonly IunitOfwork _unitOfWork;

        public FuncionarioService(IunitOfwork iunit0Fwork, IMapper mapper, IFuncionarioRespository funcionarioRespository)
        {
            _unitOfWork = iunit0Fwork ?? throw new ArgumentNullException(nameof(iunit0Fwork));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _funcionarioRespository = funcionarioRespository ?? throw new ArgumentNullException(nameof(funcionarioRespository));
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

            var funcionarioProfile = _mapper.Map<Funcionario>(createRequest);

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

        public Task<IEnumerable<Funcionario>> GetAllAsync(Guid FuncionarioId)
        {
            var funcionarios = _funcionarioRespository.GetAllAsync(FuncionarioId);
            if (funcionarios == null)
            {
                throw new Exception("Nenhum funcionario encontrado.");
            }

            return funcionarios;

        }

        public async Task<Funcionario> GetByIdAsync(Guid FuncionarioId)
        {
            var funcionario = await _funcionarioRespository.GetByIdAsync(FuncionarioId);
            if (funcionario == null)
            {
                throw new Exception("Funcionario não encontrado.");
            }

            await _unitOfWork.SaveChangesAsync();
            return funcionario;
        }

        public async Task<Funcionario> UpdateAsync(Guid id, UpdateFuncionarioRequest updateRequest)
        {
            var existingFuncionario = _funcionarioRespository.GetByIdAsync(id);
            if (existingFuncionario == null)
            {
                throw new Exception("Funcionario não encontrado.");
            }

            var funcionarioProfile = _mapper.Map<Funcionario>(updateRequest);
            await _funcionarioRespository.UpdateAsync(funcionarioProfile);

            await _unitOfWork.SaveChangesAsync();

            return funcionarioProfile;
        }
    }
}
