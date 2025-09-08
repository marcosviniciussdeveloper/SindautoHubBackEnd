using Microsoft.EntityFrameworkCore;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Interfaces;
using SindautoHub.Infrastructure.Persistance.Database;


namespace SindautoHub.Infrastructure.Persistence.Repository;

public class ChatMessageRepository : IChatMessageRepository
{
    private readonly SindautoHubContext _context;

    public ChatMessageRepository(SindautoHubContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(ChatMessage message)
    {
        await _context.ChatMessages.AddAsync(message);
    }

    public async Task<IEnumerable<ChatMessage>> GetMessagesByChatIdAsync(Guid chatId)
    {
        return await _context.ChatMessages
                             .Where(m => m.ChatId == chatId)
                             .Include(m => m.Sender) 
                             .OrderBy(m => m.SentAt) 
                             .ToListAsync();
    }
}