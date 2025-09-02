using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Domain.Entities.Enums;
using SindautoHub.Domain.Interface;

namespace SindautoHub.Domain.Entities.Models
{
    public class Funcionario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }

        public string Email { get; set; }

        public string? FotoUrl { get; set; }


        public TimeOnly? HorarioInicio { get; set; }

        
        public TimeOnly? HorarioFim { get; set; }

        public TipoContratacao TipoContratacao { get; set; }

        public Guid SetorId { get;set; }

        public Guid CargoId { get; set; }

        public Setor setor { get; set; }

        public Cargo cargo  { get; set; }

        public ICollection<Notificacao> Notificacoes { get; set; } = new List<Notificacao>();
        public ICollection<Postagem> Postagens { get; set; } = new List<Postagem>();
     }
}