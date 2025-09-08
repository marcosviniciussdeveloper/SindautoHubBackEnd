using SindautoHub.Application.Dtos.AnnouncementDtos;

namespace SindautoHub.Application.Interface
{
    public interface IAnnouncementService
    {
        Task<AnnouncementResponse> CreateAsync(CreateAnnouncementRequest request, Guid authorId);
        Task<AnnouncementResponse> UpdateAsync(UpdateAnnouncementRequest request);
        Task<bool> DeleteAsync(Guid id);
        Task<List<AnnouncementResponse>> GetAllAsync();
        Task<AnnouncementResponse> GetByIdAsync(Guid id);
    }
}
