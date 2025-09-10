using SindautoHub.Application.Dtos.ChatMessageDtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Interfaces; 

public class ChatMessageService : IChatMessageService
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IunitOfwork _unitOfWork;
    private readonly IChatNotifier _chatNotifier;

    public ChatMessageService(
        IChatMessageRepository chatMessageRepository,
        IunitOfwork unitOfWork,
        IChatNotifier chatNotifier) 
    {
        _chatMessageRepository = chatMessageRepository;
        _unitOfWork = unitOfWork;
        _chatNotifier = chatNotifier;
    }

    public async Task<ChatMessageResponse> SendMessageAsync(Guid chatId, Guid senderId, SendChatMessageRequest request)
    {
        var message = new ChatMessage
        {
            Id = Guid.NewGuid(),
            ChatId = chatId,
            SenderId = senderId,
            MessageText = request.MessageText,
            SentAt = DateTime.UtcNow,
            IsRead = false
        };

        await _chatMessageRepository.CreateAsync(message);
        await _unitOfWork.SaveChangesAsync();

      
        await _chatNotifier.NotifyMessageAsync(senderId, $"Nova mensagem no chat {chatId}");

        return new ChatMessageResponse
        {
            MessageText = message.MessageText,
            SentAt = message.SentAt,
            SenderId = message.SenderId,
            SenderName = "Você" 
        };



    }

    public async Task<List<ChatMessageResponse>> GetMessagesByChatIdAsync(Guid chatId)
    {
        var messages = await _chatMessageRepository.GetMessagesByChatIdAsync(chatId);

        return messages.Select(m => new ChatMessageResponse
        {
            MessageText = m.MessageText,
            SentAt = m.SentAt,
            SenderId = m.SenderId,
            SenderName = m.Sender?.Name
        }).ToList();
    }


}
