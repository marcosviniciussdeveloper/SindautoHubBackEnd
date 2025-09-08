using System;

namespace SindautoHub.Domain.Entities;

public class ChatMessage
{
    public Guid Id { get; set; }
    public string MessageText { get; set; }
    public DateTime SentAt { get; set; }
    
 
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; }

    public Guid SenderId { get; set; }
    public User Sender { get; set; }
}