using AutoMapper;
using SindautoHub.Application.Dtos.TicketDtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Interface;
using SindautoHub.Domain.Interfaces;

namespace SindautoHub.Application.Service;

public class TicketService : ITicketService
{
    private readonly ITicketsRepository _ticketRepo;
    private readonly ITicketMessageRepository _messageRepo;
    private readonly IUserRepository _userRepo;
    private readonly IunitOfwork _unitOfWork;
    private readonly IMapper _mapper;

    public TicketService(
        ITicketsRepository ticketRepo,
        ITicketMessageRepository messageRepo,
        IUserRepository userRepo,
        IunitOfwork unitOfWork,
        IMapper mapper)
    {
        _ticketRepo = ticketRepo;
        _messageRepo = messageRepo;
        _userRepo = userRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TicketResponse> CreateAsync(CreateTicketRequest request, Guid clienteId)
    {
        var ticket = new Ticket
        {
            Id = Guid.NewGuid(),
            Subject = request.Subject,
            StatusTicket = "Open",
            Priority = request.Priority,
            IsInternal = request.IsInternal,
            ClienteId = clienteId,
            AgenteId = request.AssignedTo,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Messages = new List<TicketMessage>()
        };

        var message = new TicketMessage
        {
            Id = Guid.NewGuid(),
            TicketId = ticket.Id,
            SenderId = clienteId,
            SenderType = "Client",
            MessageText = request.InitialMessage,
            SentAt = DateTime.UtcNow
        };

        ticket.Messages.Add(message);

        await _ticketRepo.CreateAsync(ticket);
        await _messageRepo.CreateAsync(message);
        await _unitOfWork.SaveChangesAsync();

        var fullTicket = await _ticketRepo.GetByIdWithDetailsAsync(ticket.Id);
        return _mapper.Map<TicketResponse>(fullTicket);
    }

    public async Task<List<TicketResponse>> GetAllAsync()
    {
        var tickets = await _ticketRepo.GetAllAsync();
        return _mapper.Map<List<TicketResponse>>(tickets);
    }

    public async Task<TicketResponse?> GetByIdAsync(Guid id)
    {
        var ticket = await _ticketRepo.GetByIdWithDetailsAsync(id);
        return ticket == null ? null : _mapper.Map<TicketResponse>(ticket);
    }

    public async Task<List<TicketResponse>> GetByClienteIdAsync(Guid clienteId)
    {
        var tickets = await _ticketRepo.GetTicketsByClienteIdAsync(clienteId);
        return _mapper.Map<List<TicketResponse>>(tickets);
    }

    public async Task<List<TicketResponse>> GetByAgentIdAsync(Guid agenteId)
    {
        var tickets = await _ticketRepo.GetTicketsByAgenteIdAsync(agenteId);
        return _mapper.Map<List<TicketResponse>>(tickets);
    }

    public async Task<TicketResponse?> AssignTicketAsync(Guid ticketId, Guid agentId)
    {
        var ticket = await _ticketRepo.GetByIdAsnyc(ticketId);
        if (ticket == null) return null;

        ticket.AgenteId = agentId;
        ticket.StatusTicket = "InProgress";
        ticket.UpdatedAt = DateTime.UtcNow;

        await _ticketRepo.UpdateAsAsync(ticket);
        await _unitOfWork.SaveChangesAsync();

        var updated = await _ticketRepo.GetByIdWithDetailsAsync(ticket.Id);
        return _mapper.Map<TicketResponse>(updated);
    }

    public async Task<TicketResponse?> UpdateStatusAsync(Guid ticketId, string newStatus)
    {
        var ticket = await _ticketRepo.GetByIdAsnyc(ticketId);
        if (ticket == null) return null;

        ticket.StatusTicket = newStatus;
        ticket.UpdatedAt = DateTime.UtcNow;

        await _ticketRepo.UpdateAsAsync(ticket);
        await _unitOfWork.SaveChangesAsync();

        var updated = await _ticketRepo.GetByIdWithDetailsAsync(ticket.Id);
        return _mapper.Map<TicketResponse>(updated);
    }

    public async Task DeleteAsync(Guid id)
    {
        var ticket = await _ticketRepo.GetByIdAsnyc(id);
        if (ticket == null)
            throw new Exception("Ticket não encontrado.");

        await _ticketRepo.DeleteAsync(ticket);
        await _unitOfWork.SaveChangesAsync();
    }
}