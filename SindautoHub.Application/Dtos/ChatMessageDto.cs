namespace SindautoHub.Application.Dtos.ChatMessageDtos;

public class SendChatMessageRequest
{
    public string MessageText { get; set; }
    // O SenderId virá do token e o ChatId da rota
}

public class ChatMessageResponse
{
    public Guid Id { get; set; }
    public string MessageText { get; set; }
    public DateTime SentAt { get; set; }
    public Guid SenderId { get; set; }
    public string SenderName { get; set; }
}