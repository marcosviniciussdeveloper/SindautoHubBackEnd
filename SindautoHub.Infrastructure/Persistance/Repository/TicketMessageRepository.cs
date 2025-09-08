using Microsoft.EntityFrameworkCore;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Interfaces;
using SindautoHub.Infrastructure.Persistance.Database;


namespace SindautoHub.Infrastructure.Persistence.Repository;

public class TicketMessageRepository : ITicketMessageRepository
{
    private readonly SindautoHubContext _context;

    public TicketMessageRepository(SindautoHubContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(TicketMessage message)
    {
        await _context.TicketMessages.AddAsync(message);
    }

    public async Task<TicketMessage?> GetByIdAsync(Guid messageId)
    {
        return await _context.TicketMessages.FindAsync(messageId);
    }

    public async Task<IEnumerable<TicketMessage>> GetMessagesByTicketIdAsync(Guid ticketId)
    {
        return await _context.TicketMessages
                             .Where(m => m.TicketId == ticketId)
                             .Include(m => m.Sender) 
                             .ToListAsync();
    }
}