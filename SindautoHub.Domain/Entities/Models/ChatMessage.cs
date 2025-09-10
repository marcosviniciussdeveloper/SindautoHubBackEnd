using System;
using SindautoHub.Domain.Entities.Enums;

namespace SindautoHub.Domain.Entities;

public class ChatMessage
{
    public Guid Id { get; set; }
    public string MessageText { get; set; }
    public DateTime SentAt { get; set; }

    public bool IsRead { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; } = DeliveryStatus.Enviado;
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; }

    public Guid SenderId { get; set; }
    public User Sender { get; set; }
}