using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Entities.Enums
{
    public enum  Status
    {
        Ativo = 1,
        Inativo = 2,
        Pendente = 3
    }

    public enum StatusTicket
    {
        Aberto = 1,
        EmAndamento = 2,
        Fechado = 3,
        Pendente = 4,
        Cancelado = 5
    }

    public enum  PriorityLevel
    {
        Baixa = 1,
        Media = 2,
        Alta = 3,
        Urgente = 4
    }
}
