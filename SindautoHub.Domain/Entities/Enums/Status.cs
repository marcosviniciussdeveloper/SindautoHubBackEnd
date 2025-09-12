using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Domain.Entities.Enums
{
    public enum Status
    {
        Inativo = 0,
        Ativo = 1
    }

    public enum StatusChat
    {
        Ativo = 1,
        Encerrado = 2

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



    public enum DeliveryStatus
    {
        Enviado = 0,
        Lida = 1,
        Entregue = 2
    }

    public enum PresenceStatus
    {
        Online = 1,
        Offline = 2, 
        Ausente = 3 


    }
}
