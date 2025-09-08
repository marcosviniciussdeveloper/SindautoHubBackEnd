using SindautoHub.Application.Dtos.TicketMessageDtos;

namespace SindautoHub.Application.Dtos.TicketDtos;

public class CreateTicketRequest
{
    public string Subject { get; set; }
    public string InitialMessage { get; set; }
    // O ClienteId virá do usuário logado ou será identificado pelo WhatsApp
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
    public string Status { get; set; }

    public string ClienteName { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public Guid? AssignedTo { get; set; }
    public string? AgentName { get; set; }
    public List<TicketMessageResponse> Messages { get; set; }
}
