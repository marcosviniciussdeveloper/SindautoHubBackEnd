using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Entities.Models
{
     public class ChatMessages
    {
        public Guid ID { get; set; } = Guid.NewGuid(); 

          public Guid UsuarioId { get; set; } = Guid.NewGuid();

        public ICollection<Chats> Chats { get; set; } = new List<Chats>();

       
    }
}
