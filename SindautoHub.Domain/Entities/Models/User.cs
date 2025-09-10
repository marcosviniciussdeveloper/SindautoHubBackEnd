using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Enums;
using SindautoHub.Domain.Entities.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? WhatsappNumber { get; set; }

    public Status Status { get; set; } = Status.Ativo;

    public  PresenceStatus PresenceStatus { get; set; } = PresenceStatus.Online;
    public string Cpf { get; set; }
    public string Password { get; set; } // Guardará o Hash da senha
    public string Role { get; set; }     // "Admin", "Agent", "Client", etc.
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid? PositionId { get; set; }
    public Position? Position { get; set; }

    public Guid? SectorId { get; set; }
    public Sector? Sector { get; set; }

    public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
    public ICollection<Ticket> TicketsAsCliente { get; set; } = new List<Ticket>();
    public ICollection<Ticket> TicketsAsAgente { get; set; } = new List<Ticket>();
    public ICollection<TicketMessage> TicketMessages { get; set; } = new List<TicketMessage>();
    public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
}
