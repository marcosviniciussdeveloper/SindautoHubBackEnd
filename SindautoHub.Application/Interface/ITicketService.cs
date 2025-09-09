using SindautoHub.Application.Dtos.TicketDtos;

namespace SindautoHub.Application.Interface
{
    public interface ITicketService
    {
        Task<TicketResponse> CreateAsync(CreateTicketRequest request, Guid clienteId);
        Task<TicketResponse?> GetByIdAsync(Guid id);
        Task<List<TicketResponse>> GetAllAsync();
        Task DeleteAsync(Guid id);
        Task<List<TicketResponse>> GetByClienteIdAsync(Guid clienteId);
        Task<List<TicketResponse>> GetByAgentIdAsync(Guid agenteId);
        Task<TicketResponse?> AssignTicketAsync(Guid ticketId, Guid agentId);
        Task<TicketResponse?> UpdateStatusAsync(Guid ticketId, string newStatus);
    }
}
