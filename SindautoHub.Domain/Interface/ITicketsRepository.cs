using SindautoHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Interface
{
    public interface ITicketsRepository
    {
        Task<Ticket?> GetByIdWithDetailsAsync(Guid ticketId);
        Task<IEnumerable<Ticket>> GetTicketsByAgenteIdAsync(Guid agenteId);


        Task<IEnumerable<Ticket>> GetTicketsByClienteIdAsync(Guid clienteId);
        Task<Ticket?> GetByIdAsnyc (Guid TicketId);
        Task<IEnumerable<Ticket>> GetAllAsync();

        Task CreateAsync( Ticket ticket);

        Task DeleteAsync( Ticket ticket);
        Task UpdateAsAsync(Ticket ticket);
    }
}
