using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interface;
using SindautoHub.Infrastructure.Persistance.Database;

namespace SindautoHub.Infrastructure.Persistance.Repository
{
   
    public class SetorRepository : ISetoresRepository
    {
        private readonly SindautoHubContext _context;

        public SetorRepository(SindautoHubContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Setor> CreateAsync(Setor setores)
        {
           await _context.Set<Setor>().AddAsync(setores);
            return new Setor();
        }

        public Task<bool> DeleteAsync(Guid setoresId)
        {
           var setorToDelete =  _context.Setores.Find(setoresId);
            if (setorToDelete is null)
            {
                return Task.FromResult(false);
            }
            _context.Setores.Remove(setorToDelete);
            return Task.FromResult(true);
        }

        public async Task<IEnumerable<Setor>> GetAllAsync(Guid setoresId)
        {
            return await _context.Set<Setor>().ToListAsync();
        }

        public async Task<Setor> GetByIdAsync(Guid setoresId)
        {
            return await _context.Setores.FindAsync(setoresId);

        }

        public async Task<Setor> UpdateAsync(Setor  SetorId)
        {
            _context.Update(SetorId);
            return await Task.FromResult(SetorId);
        }
    }
}
