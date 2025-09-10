using SindautoHub.Domain.Entities;

public class ChatUser
{
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastReadAt { get; set; } 
   // public bool IsAdmin { get; set; } = false; // útil em grupos
}
