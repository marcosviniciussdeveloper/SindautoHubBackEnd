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
    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly SindautoHubContext _context;
        public NotificacaoRepository(SindautoHubContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Notificacao> CreateAsync(Notificacao notificacao)
        {
            await _context.Set<Notificacao>().AddAsync(notificacao);
                return new Notificacao();
        }

        public Task<bool> DeleteAsync(Guid notificacaoId)
        {
            var notificacaoToDelete = _context.Notificacoes.Find(notificacaoId);
            if (notificacaoToDelete is null)
            {
                return Task.FromResult(false);
            }

            _context.Notificacoes.Remove(notificacaoToDelete);
            return Task.FromResult(true);
        }

        public async Task<IEnumerable<Notificacao>> GetAllAsync(Guid FuncionarioId)
        {
            return await _context.Set<Notificacao>().ToListAsync();
        }

        public async Task<Notificacao> GetByIdAsync(Guid notificacaoId)
        {
            return await _context.Notificacoes.FindAsync(notificacaoId);
        }

        public Task<Notificacao> UpdateAsync(Notificacao NotificacaoId)
        {
            _context.Update(NotificacaoId);
            return Task.FromResult(NotificacaoId);
        }
    }
}
