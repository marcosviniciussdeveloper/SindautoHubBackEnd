using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Entities.Models
{
    public class Chats
    {
        public Guid Id { get; set; } = Guid.NewGuid();  

        public Guid UserId { get; set; } = Guid.NewGuid();

        public string Message { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new List<User>();

        public ICollection<ChatMessages> ChatMessages { get; set; } = new List<ChatMessages>();

    }
}
