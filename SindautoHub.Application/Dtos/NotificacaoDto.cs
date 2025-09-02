using System;

namespace SindautoHub.Application.Dtos;

public class CreateNotificacaoRequest
{
    public string Mensagem { get; set; }
    public string? LinkDestino { get; set; } 
    public Guid UsuarioId { get; set; }
}


public class UpdateNotificacaoRequest
{
 
    public bool Lida { get; set; }
}


public class NotificacaoResponse
{
    public Guid Id { get; set; }
    public string Mensagem { get; set; }
    public bool Lida { get; set; }
    public string? LinkDestino { get; set; }
    public DateTime DataCriacao { get; set; }
    public Guid UsuarioId { get; set; }
}