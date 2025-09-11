using SindautoHub.Application.Dtos.ChatDtos;
using SindautoHub.Application.Dtos.ChatMessageDtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Interfaces;
using SindautoHub.Domain.Entities.Enums;

public class ChatService : IChatServices
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IunitOfwork _unitOfWork;

    public ChatService(
        IChatRepository chatRepository,
        IUserRepository userRepository,
        IChatMessageRepository chatMessageRepository,
        IunitOfwork unitOfWork)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _chatMessageRepository = chatMessageRepository;
    }

    public async Task<ChatResponse> CreateChatAsync(CreateChatRequest request)
    {
        if (request.ParticipantIds == null || !request.ParticipantIds.Any())
            throw new ArgumentException("É necessário pelo menos um participante para criar o chat.");

       
        var chat = new Chat
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastMessageAt = DateTime.UtcNow,
            StatusChat = Status.Ativo
        };

        
        foreach (var participantId in request.ParticipantIds.Distinct())
        {
            var user = await _userRepository.GetByIdAsync(participantId);
            if (user == null)
                throw new ArgumentException($"Usuário com ID {participantId} não encontrado.");

            chat.ChatUsers.Add(new ChatUser
            {
                ChatId = chat.Id,
                UserId = user.Id
            });
        }

        await _chatRepository.CreateAsync(chat);
        await _unitOfWork.SaveChangesAsync();

        return MapToChatResponse(chat);
    }

    public async Task<List<ChatResponse>> GetChatsByUserIdAsync(Guid userId)
    {
        var chats = await _chatRepository.GetChatsByUserIdAsync(userId);

        return chats.Select(MapToChatResponse).ToList();
    }

    public async Task<ChatResponse> GetChatByIdAsync(Guid chatId)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        if (chat == null)
            throw new KeyNotFoundException("Chat não encontrado.");

        return MapToChatResponse(chat);
    }


    private ChatResponse MapToChatResponse(Chat chat)
    {
        return new ChatResponse
        {
            Id = chat.Id,
            CreatedAt = chat.CreatedAt,
            Participants = chat.ChatUsers.Select(cu => new UserSummaryResponse
            {
                Id = cu.User.Id,
                Name = cu.User.Name
            }).ToList(),
            Messages = chat.Messages?.Select(m => new ChatMessageResponse
            {
               
                MessageText = m.MessageText,  
                SentAt = m.SentAt,
                SenderId = m.SenderId,
                SenderName = m.Sender?.Name
            }).ToList() ?? new List<ChatMessageResponse>()

        };
    }

    public async Task<ChatMessageResponse> SendMessageAsync(Guid chatId, Guid senderId, SendChatMessageRequest request)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        if (chat == null)
            throw new KeyNotFoundException("Chat não encontrado.");

        if (!chat.ChatUsers.Any(cu => cu.UserId == senderId))
            throw new UnauthorizedAccessException("Usuário não faz parte deste chat.");

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

        chat.LastMessageAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();

        return new ChatMessageResponse
        {
            Id = message.Id,
            MessageText = message.MessageText,
            SentAt = message.SentAt,
            SenderId = message.SenderId,
  
            SenderName = chat.ChatUsers.FirstOrDefault(cu => cu.UserId == senderId)?.User.Name
                         ?? "Usuário desconhecido"
        };
    }
}


