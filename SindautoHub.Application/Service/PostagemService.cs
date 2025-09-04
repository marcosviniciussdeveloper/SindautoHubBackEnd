using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Application.Service
{
    public class PostagemService : IPostagemService
    {
        public Task<Postagem> CreateAsync(CreatePostagemRequest CreatePostagemRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Postagem postagemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Postagem>> GetAllAsync(Guid postagemId)
        {
            throw new NotImplementedException();
        }

        public Task<Postagem> GetByIdAsync(Guid postagemId)
        {
            throw new NotImplementedException();
        }

        public Task<Postagem> UpdateAsync(Guid Id, UpdatePostagemRequest updatePostagem)
        {
            throw new NotImplementedException();
        }
    }
}
