using SindautoHub.Application.Dtos.ChatMessageDtos;

namespace SindautoHub.Application.Interface
{
    public interface IChatMessageService
    {
        Task<List<ChatMessageResponse>> GetMessagesByChatIdAsync(Guid chatId);
        Task<ChatMessageResponse> SendMessageAsync(Guid chatId, Guid senderId, SendChatMessageRequest request);
    }
}
