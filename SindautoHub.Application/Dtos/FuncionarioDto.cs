using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Domain.Entities.Enums;

namespace SindautoHub.Application.Dtos
{
    public class CreateFuncionarioRequest
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string? FotoUrl { get; set; }

        
        public string Cpf { get; set; }
        public TimeOnly? HorarioInicio { get; set; }

        public TimeOnly? HorarioFim { get; set; }

        public Guid SetorId { get; set; }

        public Guid CargoId { get; set; }

        public TipoContratacao TipoContratacao { get; set; }

    }

    public class UpdateFuncionarioRequest
    {
        public string ?Nome { get; set; }
        public string? Email { get; set; }
        public string? FotoUrl { get; set; }
        public string ?Cpf { get; set; }
        public TimeOnly? HorarioInicio { get; set; }
        public TimeOnly? HorarioFim { get; set; }
        public Guid? SetorId { get; set; }
        public Guid ?CargoId { get; set; }

        public TipoContratacao? TipoContratacao { get; set; }
    }

    public class FuncionarioResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? FotoUrl { get; set; }
        public string Cpf { get; set; }
        public TimeOnly? HorarioInicio { get; set; }
        public TimeOnly? HorarioFim { get; set; }
        public string NomeDoCargo { get; set; }
        public string NomeDoSetor { get; set; }
        public  string TipoContratacao { get; set; }
    }

}
