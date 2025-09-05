using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Entities.Models
{
    public class Announcements
    {
        public Guid Id { get; set; } = new Guid();

        public Guid UsersId { get; set; }

        public Guid PostedBy { get; set; }
        public DateTime Created { get; set; }

        public string Mensagem { get; set; }

        public string? LinkDestino { get; set; }

        public TimeOnly DataCriacao { get; set; }

    }
}
