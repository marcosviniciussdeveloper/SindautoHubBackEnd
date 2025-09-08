using SindautoHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Interfaces;

public interface ITicketMessageRepository
{
    Task<TicketMessage?> GetByIdAsync(Guid messageId);
    Task<IEnumerable<TicketMessage>> GetMessagesByTicketIdAsync(Guid ticketId);
    Task CreateAsync(TicketMessage message);
}