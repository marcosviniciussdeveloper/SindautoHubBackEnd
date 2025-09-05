using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Application.Service
{
    public class PostagemService : IPostagemService
    {
        private readonly IunitOfwork _unitOfWork;
        private readonly IPostagensRepository _postagemRepository;
        private readonly IMapper _mapper;

        public PostagemService(IunitOfwork unitOfWork, IPostagensRepository postagemRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _postagemRepository = postagemRepository;
            _mapper = mapper;
        }

        public async Task<Postagem> CreateAsync(CreatePostagemRequest createPostagemRequest)
        {
            var postagem = _mapper.Map<Postagem>(createPostagemRequest);
            if (postagem == null)
            {
                throw new Exception("Erro ao criar a postagem.");
            }

            await _postagemRepository.CreateAsync(postagem);

            await _unitOfWork.SaveChangesAsync();

            return postagem;
        }

        public async Task<bool> DeleteAsync(Postagem postagemId)
        {
            var existingPostagem = _postagemRepository.GetByIdAsync(postagemId.Id).Result;
            if (existingPostagem == null)
            {
                throw new Exception("Postagem não encontrada.");
            }

            return true;
        }

        public async Task<IEnumerable<Postagem>> GetAllAsync(Guid postagemId)
        {
            var postagem = _postagemRepository.GetAllAsync(postagemId);
            if (postagem == null)
            {
                throw new Exception("Postagem não encontrada.");
            }

            await _unitOfWork.SaveChangesAsync();

            return await postagem;

        }

        public async Task<Postagem> GetByIdAsync(Guid postagemId)
        {
            var funcionario = await _postagemRepository.GetByIdAsync(postagemId);
            if (funcionario == null)
            {
                throw new Exception("Postagem não encontrada.");
            }
            await _unitOfWork.SaveChangesAsync();
            return funcionario;

        }

        public async Task<Postagem> UpdateAsync(Guid Id, UpdatePostagemRequest updatePostagem)
        {
            var funcionarioEntity = _postagemRepository.GetByIdAsync(Id).Result;
            if (funcionarioEntity == null)
            {
                throw new Exception("Postagem não encontrada.");
            }

            funcionarioEntity.Titulo = updatePostagem.Titulo;
            funcionarioEntity.Conteudo = updatePostagem.Conteudo;

            _postagemRepository.UpdateAsync(funcionarioEntity);
            await _unitOfWork.SaveChangesAsync();

            return funcionarioEntity;
        }
    }
}
