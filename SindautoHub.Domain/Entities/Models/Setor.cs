using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Entities.Models
{
    public class Setor
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string NomeSetor { get; set; }  
        
        public string Descricao { get; set; }


        public DateTime HorarioFuncionamento { get; set; }


        public ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
    }
}
