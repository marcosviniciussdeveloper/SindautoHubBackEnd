using SindautoHub.Application.Dtos.ChatDtos;
using SindautoHub.Application.Dtos.ChatMessageDtos;

namespace SindautoHub.Application.Interface
{
    public interface IChatServices
    {
        Task<ChatResponse> CreateChatAsync(CreateChatRequest request);
        Task<List<ChatResponse>> GetChatsByUserIdAsync(Guid userId);
        Task<ChatResponse> GetChatByIdAsync(Guid chatId);
        Task<ChatMessageResponse> SendMessageAsync(Guid chatId, Guid senderId, SendChatMessageRequest request);
    }
}
