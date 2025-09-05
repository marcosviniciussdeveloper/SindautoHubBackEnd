using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Domain.Entities.Enums;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Domain.Entities.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public DateTime DataNascimento { get; set; }

        public string? FotoUrl { get; set; }

        public string Cpf { get; set; }


        // public TimeOnly? HorarioInicio { get; set; }


        // public TimeOnly? HorarioFim { get; set; }

        public TipoContratacao TipoContratacao { get; set; }

        public Guid SetorId { get; set; }

        public Guid CargoId { get; set; }

        public Setor Setor { get; set; }

        public string Password { get; set; }

        public Cargo Cargo { get; set; }

        public ICollection<TicketMessages> TicketMessages { get; set; } = new List<TicketMessages>();

        public ICollection<ChatMessages> ChatMessages { get; set; } = new List<ChatMessages>();

        public ICollection<Chats> Chats { get; set; } = new List<Chats>();
        public ICollection<Tickets> Tickets { get; set; } = new List<Tickets>();

        public ICollection<Announcements> Announcements { get; set; } = new List<Announcements>();
    }
}