using SindautoHub.Application.Dtos.TicketMessageDtos;
using SindautoHub.Domain.Entities.Enums;

namespace SindautoHub.Application.Dtos.TicketDtos;

public class CreateTicketRequest
{
    public string Subject { get; set; }
    public string InitialMessage { get; set; }

    public PriorityLevel Priority { get; set; } // Ex: "Baixa", "Média", "Alta", "Urgente"
    public bool IsInternal { get; set; }

  
    public Guid? AssignedTo { get; set; }
}

public class AssignTicketRequest
{
    public Guid AgentId { get; set; }
}

public class UpdateTicketStatusRequest
{
    public string NewStatus { get; set; } // "Open", "InProgress", "Resolved"
}
public class TicketResponse
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string Status { get; set; } // Open, InProgress, Resolved

    public string Priority { get; set; }
    public bool IsInternal { get; set; }

    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string ClienteName { get; set; }

    public Guid? AssignedTo { get; set; }
    public string? AgentName { get; set; }

    public DateTime CreatedAt { get; set; }
    public List<TicketMessageResponse> Messages { get; set; }
}

