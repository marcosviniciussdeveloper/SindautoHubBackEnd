
using Microsoft.EntityFrameworkCore;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interface;
using SindautoHub.Infrastructure.Persistance.Database;

namespace SindautoHub.Infrastructure.Persistance.Repository
{
    public  class UsersRepository :IUsersRespository
    {
        private readonly SindautoHubContext _context;


        public  UsersRepository(SindautoHubContext context)
        {


            _context = context;


        }

        public async Task<User> CreateAsync(User funcionario)
        {
            await _context.Set<User>().AddAsync(funcionario);
            return new User();
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

        public async Task<IEnumerable<User>> GetAllAsync(Guid FuncionarioId)
        {
            return await  _context.Set<User>().ToListAsync();

        }

        public async Task<User> GetByCpfAsync(string cpf)
        {
            return await _context.Funcionarios.FirstOrDefaultAsync(f => f.Cpf == cpf);
          
        }

        public async Task<User> GetByEmailAsync(string email)
        {
           return await _context.Funcionarios.FirstOrDefaultAsync(f => f.Email == email);
        }

        public async Task<User> GetByIdAsync(Guid funcionarioId)
        {
            return await _context.Funcionarios.FindAsync(funcionarioId);
        }

        public async Task<User?> GetByIdWithincludesAsync(Guid id)
        {
            return await _context.Funcionarios
                .Include(f => f.cargo)
                .Include(f => f.setor)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<User> UpdateAsync(User FuncionarioId)
        {
            _context.Update(FuncionarioId);
            return await Task.FromResult(FuncionarioId);
        }

       
    }
}
