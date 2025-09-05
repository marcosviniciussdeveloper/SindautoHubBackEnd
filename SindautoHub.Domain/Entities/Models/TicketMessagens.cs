using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Entities.Models
{
    public class TicketMessages
    {
        public Guid ID { get; set; } = Guid.NewGuid(); // Identificador único da mensagem

        public Guid TicketId { get; set; } // Chave estrangeira para o ticket

        public Guid AgentId { get; set; } // Chave estrangeira para o usuário que enviou a mensagem

        public string MensagemText { get; set; }

        public DateTime DataEnvio { get; set; }

        public ICollection<Tickets> Icollecction { get; set; } = new List<Tickets>();

    }
    }
