using SindautoHub.Domain.Entities.Enums;

namespace SindautoHub.Application.Dtos.UserDtos;

public class CreateUserRequest
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } // "Admin", "Agent", "Client"

    public Guid? PositionId { get;set;}
    public Guid? SectorId { get;set;}
}

public class UpdateUserRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string Status { get; set; }
    public string? Password { get; set; }

    public string? UserName { get; set; }
    public string? Role { get; set; }
}

public class UserResponse
{
  
   public Guid Id { get; set; }
    public string Name { get; set; }

    public Status Status { get; set; }
 
    public string PositionName { get; set; }
    public string SectorName {get;set;}
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
}