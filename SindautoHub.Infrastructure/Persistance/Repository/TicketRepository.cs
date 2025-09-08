using Microsoft.EntityFrameworkCore;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Interface;
using SindautoHub.Domain.Interfaces;
using SindautoHub.Infrastructure.Persistance.Database;


namespace SindautoHub.Infrastructure.Persistence.Repository;

public class TicketRepository : ITicketsRepository
{
    private readonly SindautoHubContext _context;

    public TicketRepository(SindautoHubContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
    }

    public Task DeleteAsync(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        return await _context.Tickets
                             .Include(t => t.Cliente)
                             .Include(t => t.Agent)
                             .ToListAsync();
    }

    public async Task<Ticket?> GetByIdAsnyc(Guid TicketId)
    {
       return await _context.Tickets.FindAsync(TicketId) ;
    }

    public async Task<Ticket?> GetByIdAsync(Guid ticketId)
    {
        return await _context.Tickets.FindAsync(ticketId);
    }

    public async Task<Ticket?> GetByIdWithDetailsAsync(Guid ticketId)
    {
        return await _context.Tickets
                             .Include(t => t.Cliente)
                             .Include(t => t.Agent)
                             .Include(t => t.Messages)
                                .ThenInclude(m => m.Sender) 
                             .FirstOrDefaultAsync(t => t.Id == ticketId);
    }

    public async Task<IEnumerable<Ticket>> GetTicketsByAgenteIdAsync(Guid agenteId)
    {
        return await _context.Tickets
                             .Where(t => t.AgenteId == agenteId)
                             .Include(t => t.Cliente)
                             .OrderByDescending(t => t.UpdatedAt)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetTicketsByClienteIdAsync(Guid clienteId)
    {
        return await _context.Tickets
                             .Where(t => t.ClienteId == clienteId)
                             .Include(t => t.Agent)
                             .OrderByDescending(t => t.CreatedAt)
                             .ToListAsync();
    }

    public Task UpdateAsAsync(Ticket ticket)
    {
       _context.Tickets.Update(ticket);
        return Task.CompletedTask;
    }
}