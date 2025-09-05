using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace SindautoHub.Domain.Entities.Models
{


    public class Cargo
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Nome { get; set; }

        public string DescricaoAtribuicoes { get; set; }


        public ICollection<User> Funcionarios { get; set; } = new List<User>();
    }


}

