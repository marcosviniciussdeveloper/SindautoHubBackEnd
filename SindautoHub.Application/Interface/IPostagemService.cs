
using SindautoHub.Application.Dtos;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Application.Interface
{
    public interface IPostagemService
    {
        Task<Postagem> CreateAsync(CreatePostagemRequest CreatePostagemRequest);

        Task<IEnumerable<Postagem>> GetAllAsync(Guid postagemId);

        Task<Postagem> GetByIdAsync(Guid postagemId);

        Task<Postagem> UpdateAsync(Guid Id , UpdatePostagemRequest  updatePostagem);

        Task<bool> DeleteAsync(Postagem postagemId);

    }
}
