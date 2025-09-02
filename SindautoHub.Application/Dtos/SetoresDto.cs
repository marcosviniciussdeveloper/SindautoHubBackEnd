using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Application.Dtos
{
    public class CreateSetorRequest
    {
        public string Nome { get; set; }
        public string? Descricao { get; set; }

        public DateTime HorarioFuncionamento { get; set; }
    }


    public  class UpdateSetorRequest
    {
        public Guid Id { get; set; }

        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? HorarioFuncionamento { get; set; }
    }



    public class SetorResponse
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string? Descricao { get; set; }

        public DateTime HorarioFuncionamento { get; set; }

        public int TotalFuncionarios { get; set; }

    }

}