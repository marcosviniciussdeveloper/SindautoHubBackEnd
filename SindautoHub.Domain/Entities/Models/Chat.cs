using System;
using System.Collections.Generic;
using SindautoHub.Domain.Entities.Enums;

namespace SindautoHub.Domain.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public Status StatusChat { get; set; } = Status.Ativo;
        public DateTime? LastMessageAt { get; set; }
        public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
        public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
    }
}