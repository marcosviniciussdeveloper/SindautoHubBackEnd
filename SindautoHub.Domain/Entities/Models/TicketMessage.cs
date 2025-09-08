using System;

namespace SindautoHub.Domain.Entities;

public class TicketMessage
{
    public Guid Id { get; set; }
    public string MessageText { get; set; }
    public DateTime SentAt { get; set; }
    public string SenderType { get; set; } // "Client" ou "Agent"

    public Guid TicketId { get; set; }
    public Ticket Ticket { get; set; }

 
    public Guid SenderId { get; set; }
    public User Sender { get; set; }
}