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
    public class PostagemRepository : IPostagensRepository
    {

        private readonly SindautoHubContext _context;

        public PostagemRepository(SindautoHubContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<Postagem> CreateAsync(Postagem postagem)
        {
            await _context.Set<Postagem>().AddAsync(postagem);
            return postagem;
        }

        public Task<bool> DeleteAsync(Postagem postagemId)
        {
            var postagemToDelete = _context.Postagens.Find(postagemId);
            if (postagemToDelete is null)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public async Task<IEnumerable<Postagem>> GetAllAsync(Guid postagemId)
        {
            return await _context.Set<Postagem>().ToListAsync();
        }

        public async Task<Postagem> GetByIdAsync(Guid postagemId)
        {
             return await _context.Postagens.FindAsync(postagemId);
        }

        public Task<Postagem> UpdateAsync(Postagem PostagemID)
        {
            _context .Postagens.Update(PostagemID);
            return Task.FromResult(PostagemID);
        }
    }
}
