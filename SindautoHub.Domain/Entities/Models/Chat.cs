using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Enums;

public class Chat
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public StatusChat StatusChat { get; set; } = StatusChat.Ativo;
    public DateTime? LastMessageAt { get; set; }
    public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
}
