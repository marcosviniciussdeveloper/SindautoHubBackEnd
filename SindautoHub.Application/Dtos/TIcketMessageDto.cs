namespace SindautoHub.Application.Dtos.TicketMessageDtos;

public class AddTicketMessageRequest
{
    public string MessageText { get; set; }
    // O SenderId virá do usuário logado
}

public class TicketMessageResponse
{
    public Guid Id { get; set; }
    public string MessageText { get; set; }
    public DateTime SentAt { get; set; }
    public Guid SenderId { get; set; }
    public string SenderName { get; set; }
    public string SenderType { get; set; }
}