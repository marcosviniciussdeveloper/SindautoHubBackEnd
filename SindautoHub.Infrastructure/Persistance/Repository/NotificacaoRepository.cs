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
    public class NotificacaoRepository : IAnnouncementsRepository
    {
        private readonly SindautoHubContext _context;
        public NotificacaoRepository(SindautoHubContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<announcements> CreateAsync(announcements notificacao)
        {
            await _context.Set<announcements>().AddAsync(notificacao);
                return new announcements();
        }

        public async Task<bool> DeleteAsync(announcements notificacao)
        {
             _context.Notificacoes.Remove(notificacao);
            return await Task.FromResult(true);
        }

        

        public async Task<IEnumerable<announcements>> GetAllAsync(Guid FuncionarioId)
        {
            return await _context.Set<announcements>().ToListAsync();
        }

        public async Task<announcements> GetByIdAsync(Guid notificacaoId)
        {
            return await _context.Notificacoes.FindAsync(notificacaoId);
        }

        public Task<announcements> UpdateAsync(announcements NotificacaoId)
        {
            _context.Update(NotificacaoId);
            return Task.FromResult(NotificacaoId);
        }
    }
}
