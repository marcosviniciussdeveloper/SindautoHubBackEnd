using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SindautoHub.Application.Common.Mappings;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Application.Service
{
    public class SetorService : ISetorService
    {
        private readonly ISetoresRepository _setorRepository;
        private readonly IMapper _mapper;
        private readonly IunitOfwork _unitOfWork;
        public SetorService(IMapper mapper, ISetoresRepository setorRepository, IunitOfwork unitOfWork)
        {
            _setorRepository = setorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Setor> CreateAsync(CreateSetorRequest createSetorRequest)
        {
            var SetorExistente = _setorRepository.GetByIdAsync(createSetorRequest.Id).Result;
            if (SetorExistente != null)
            {
                throw new Exception("Setor já existe.");
            }

            var setorProfile = _mapper.Map<Setor>(createSetorRequest);
            await _unitOfWork.SaveChangesAsync();

            var newSetorComDados = await _setorRepository.GetByIdAsync(setorProfile.Id);

            return newSetorComDados;


        }

        public async Task<bool> DeleteAsync(Guid setoresId)
        {
            var setorExistente = _setorRepository.GetByIdAsync(setoresId);
            if (setorExistente == null)
            {
                throw new Exception("Setor não encontrado.");
            }

            await _setorRepository.DeleteAsync(setoresId);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<Setor>> GetAllAsync(Guid setoresId)
        {
            var setor = _setorRepository.GetAllAsync(setoresId);
            if (setor == null)
            {
                throw new Exception("Nenhum setor encontrado.");
            }

            return setor;
        }

        public Task<Setor> GetByIdAsync(Guid setoresId)
        {
            var setor = _setorRepository.GetByIdAsync(setoresId);
            if (setor == null)
            {
                throw new Exception("Setor não encontrado.");
            }

            return setor;
        }

        public Task<Setor> UpdateAsync(Guid SetorId, UpdateSetorRequest updateSetorRequest)
        {
           var setorExistente = _setorRepository.GetByIdAsync(SetorId);
            if (setorExistente == null)
            {
                throw new Exception("Setor não encontrado.");
            }
            var setorProfile = _mapper.Map<Setor>(updateSetorRequest);
            return _setorRepository.UpdateAsync(setorProfile);
        }
    }
}
