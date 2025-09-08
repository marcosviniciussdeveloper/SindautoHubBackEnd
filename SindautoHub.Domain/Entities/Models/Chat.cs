using System;
using System.Collections.Generic;

namespace SindautoHub.Domain.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
        public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
    }
}