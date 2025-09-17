using Microsoft.EntityFrameworkCore;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Enums;
using SindautoHub.Domain.Interfaces;
using SindautoHub.Infrastructure.Persistance.Database;
using Supabase.Gotrue;

public class ChatRepository : IChatRepository
{
    private readonly SindautoHubContext _context;

    public ChatRepository(SindautoHubContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Chat chat)
    {
        await _context.Chats.AddAsync(chat);
    }

    public async Task<Chat?> GetByIdAsync(Guid chatId)
    {
        return await _context.Chats
                             .Include(c => c.ChatUsers)
                                .ThenInclude(cu => cu.User)
                             .Include(c => c.Messages)
                                .ThenInclude(m => m.Sender) 
                             .FirstOrDefaultAsync(c => c.Id == chatId);
    }
    public async Task<Chat?> GetChatByParticipantsAsync(List<Guid> participantIds)
    {
        var chat = await _context.Chats
            .Include(c => c.ChatUsers)
                .ThenInclude(cu => cu.User)
            .Include(c => c.Messages)
            .Where(c =>
                c.ChatUsers.Count == participantIds.Count &&
                c.ChatUsers.All(cu => participantIds.Contains(cu.UserId)) &&
                participantIds.All(id => c.ChatUsers.Any(cu => cu.UserId == id))
            )
            .FirstOrDefaultAsync();

        return chat;
    }




    public async Task<IEnumerable<Chat>> GetChatsByUserIdAsync(Guid userId)
    {
        return await _context.Chats
                             .Include(c => c.ChatUsers)
                                .ThenInclude(cu => cu.User)
                             .Where(c => c.ChatUsers.Any(cu => cu.UserId == userId))
                             .ToListAsync();
    }
}
