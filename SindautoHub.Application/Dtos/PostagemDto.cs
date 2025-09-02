using System;

namespace SindautoHub.Application.Dtos;


public class CreatePostagemRequest
{
    public string Titulo { get; set; }
    public string Conteudo { get; set; }
  
}



public class UpdatePostagemRequest
{
    public string? Titulo { get; set; }
    public string? Conteudo { get; set; }
}



public class PostagemResponse
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Conteudo { get; set; }
    public DateTime DataPublicacao { get; set; } 
    public Guid AutorId { get; set; }
    public string AutorNome { get; set; }
    public string? AutorFotoUrl { get; set; }
}