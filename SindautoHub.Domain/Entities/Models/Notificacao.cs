using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Entities.Models
{
    public class Notificacao
    {
        public Guid Id { get; set; } = new Guid();

        public Guid UsuarioId { get; set; }
        public Funcionario Usuario { get; set; }
        public bool Lida { get; set; }

        public string Mensagem { get; set; }

        public string? LinkDestino { get; set; }

        public TimeOnly DataCriacao { get; set; }

        public ICollection<Notificacao> Notificacoes { get; set; } = new List<Notificacao>();
    }
}
