using SindautoHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Interfaces;

public interface IChatRepository
{
    Task<Chat?> GetByIdAsync(Guid chatId);
    Task<IEnumerable<Chat>> GetChatsByUserIdAsync(Guid userId);
    Task CreateAsync(Chat chat);
}