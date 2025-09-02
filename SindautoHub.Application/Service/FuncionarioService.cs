
using System.Threading.Tasks;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Application.Service
{


    public class FuncionarioService : IFuncionarioServices_cs
    {
        private readonly IFuncionarioRespository _funcionarioRespository;
        private readonly INotificacaoServices _notificacaoServices;

        public FuncionarioService(IFuncionarioRespository funcionarioRespository, INotificacaoServices notificacaoServices)
        {
            _funcionarioRespository = funcionarioRespository ?? throw new ArgumentNullException(nameof(funcionarioRespository));
        }

        public Task<Funcionario> CreateAsync(CreateFuncionarioRequest createRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid FuncionarioId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Funcionario>> GetAllAsync(Guid FuncionarioId)
        {
            throw new NotImplementedException();
        }

        public Task<Funcionario> GetByIdAsync(Guid FuncionarioId)
        {
            throw new NotImplementedException();
        }

        public Task<Funcionario> UpdateAsync(Guid id, UpdateFuncionarioRequest updateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
