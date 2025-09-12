using SindautoHub.Domain.Entities.Enums;
using StackExchange.Redis;

namespace SindautoHub.Application.Dtos.UserDtos;

public class CreateUserRequest
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } // "Admin", "Agent", "Client"

    // Caminho relativo no Supabase
    public string? PhotoPath { get; set; }

    public Guid? PositionId { get; set; }
    public Guid? SectorId { get; set; }
}

public class UpdateUserRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public Status? Status { get; set; } // opcional no update
    public string? Password { get; set; }
    public string? UserName { get; set; }
    public string? Role { get; set; }

    public Guid? PositionId { get; set; } 
    public string? PhotoPath { get; set; }
}

public class UserResponse
{
    public Guid Id { get; set; }
    public Guid? SectorId { get; set; }

    public Guid PositionId { get; set; }
    public string? Name { get; set; }
    public  Status Status { get; set;  } 
    public string Email { get; set; }
    public string Role { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public string? PositionName { get; set; }
    public string SectorName { get; set; }

    // armazenado no banco
    public string? PhotoPath { get; set; }

    // url pública gerada pelo back
    public string? PhotoUrl { get; set; }
}

public class UserBySectorResponse
{

    
    public Guid Id { get; set; }

    public Status Status { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public bool IsOnline { get; set; }
    public string? PhotoUrl { get; set; } // avatar
}



public class UpdateStatus
{
    public Status Status {  set; get; } 

}
