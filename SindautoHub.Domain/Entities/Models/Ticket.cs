using SindautoHub.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace SindautoHub.Domain.Entities;

public class Ticket
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string StatusTicket { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public PriorityLevel Priority { get; set; }
    public Guid ClienteId { get; set; } 
    public User Cliente { get; set; }

    public bool IsInternal { get; set; }

    public Guid? AgenteId { get; set; }      
    public User? Agent { get; set; }       

    public ICollection<TicketMessage> Messages { get; set; } = new List<TicketMessage>();
}