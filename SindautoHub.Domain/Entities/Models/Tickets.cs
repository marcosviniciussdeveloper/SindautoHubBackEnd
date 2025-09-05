using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Entities.Models
{
    public class Tickets
    {
        public Guid id { get; set; } = Guid.NewGuid();

        //Chave estrangeira para o usuário que criou o ticket
        public Guid UsuarioId { get; set; }

        public string WhatsappNumber { get; set; }

        public string Subject { get; set; }

  
        public User User { get; set; }

        public ICollection<TicketMessages> TicketMessages { get; set; } = new List<TicketMessages>();

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
