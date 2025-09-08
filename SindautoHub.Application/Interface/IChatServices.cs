using SindautoHub.Application.Dtos.ChatDtos;

namespace SindautoHub.Application.Interface
{
    public interface IChatServices
    {
        Task<ChatResponse> CreateChatAsync(CreateChatRequest request);
        Task<List<ChatResponse>> GetChatsByUserIdAsync(Guid userId);
        Task<ChatResponse> GetChatByIdAsync(Guid chatId);
    }
}
