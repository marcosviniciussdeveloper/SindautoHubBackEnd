using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Entities.Models
{
    public  class Postagem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public TimeOnly DataPublicacao { get; set; }

        public Guid AutorId { get; set; } = Guid.NewGuid();

        public Funcionario Autor { get; set; }


    }
}
