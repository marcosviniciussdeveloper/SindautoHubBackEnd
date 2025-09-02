using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Application.Dtos
{
   public class CreateCargoRequest
    {
        public string Nome { get; set; }
        public string DescricaoAtribuicoes { get; set; }
    }
    public class UpdateCargoRequest
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? DescricaoAtribuicoes { get; set; }
    }
    public class CargoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string DescricaoAtribuicoes { get; set; }
    }
}
