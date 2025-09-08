
using AutoMapper;
using SindautoHub.Application.Dtos.AnnouncementDtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Interface;



namespace SindautoHub.Application.Service
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementsRepository _announcementsRepository;
        private readonly IMapper _mapper;
        private readonly IunitOfwork _iunitOfwork;

        public AnnouncementService(IAnnouncementsRepository announcementsRepository, IMapper mapper, IunitOfwork iunitOfwork)
        {
            _announcementsRepository = announcementsRepository;
           _mapper = mapper;
            _iunitOfwork = iunitOfwork;
        }

        public Task<AnnouncementResponse> CreateAsync(CreateAnnouncementRequest request, Guid authorId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AnnouncementResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AnnouncementResponse> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AnnouncementResponse> UpdateAsync(UpdateAnnouncementRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
