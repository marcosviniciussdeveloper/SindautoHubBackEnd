
using Microsoft.EntityFrameworkCore;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interface;
using SindautoHub.Infrastructure.Persistance.Database;

namespace SindautoHub.Infrastructure.Persistance.Repository
{
    public  class FuncionarioRepository :IFuncionarioRespository
    {
        private readonly SindautoHubContext _context;


        public  FuncionarioRepository(SindautoHubContext context)
        {


            _context = context;


        }

        public async Task<Funcionario> CreateAsync(Funcionario funcionario)
        {
            await _context.Set<Funcionario>().AddAsync(funcionario);
            return new Funcionario();
        }

        public Task<bool> DeleteAsync(Guid funcionarioId)
        {
            var funcionarioToDelete =  _context.Funcionarios.Find(funcionarioId);
            if (funcionarioToDelete is null)
            {
                return Task.FromResult(false);
            }

            _context.Funcionarios.Remove(funcionarioToDelete);
            return Task.FromResult(true);
        }

        public async Task<IEnumerable<Funcionario>> GetAllAsync(Guid FuncionarioId)
        {
            return await  _context.Set<Funcionario>().ToListAsync();

        }

        public async Task<Funcionario> GetByCpfAsync(string cpf)
        {
            return await _context.Funcionarios.FirstOrDefaultAsync(f => f.Cpf == cpf);
          
        }

        public async Task<Funcionario> GetByEmailAsync(string email)
        {
           return await _context.Funcionarios.FirstOrDefaultAsync(f => f.Email == email);
        }

        public async Task<Funcionario> GetByIdAsync(Guid funcionarioId)
        {
            return await _context.Funcionarios.FindAsync(funcionarioId);
        }

        public async Task<Funcionario?> GetByIdWithincludesAsync(Guid id)
        {
            return await _context.Funcionarios
                .Include(f => f.cargo)
                .Include(f => f.setor)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Funcionario> UpdateAsync(Funcionario FuncionarioId)
        {
            _context.Update(FuncionarioId);
            return await Task.FromResult(FuncionarioId);
        }

       
    }
}
