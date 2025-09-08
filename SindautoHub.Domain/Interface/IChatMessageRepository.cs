using SindautoHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Interfaces;

public interface IChatMessageRepository
{
    Task CreateAsync(ChatMessage message);
    Task<IEnumerable<ChatMessage>> GetMessagesByChatIdAsync(Guid chatId);
}