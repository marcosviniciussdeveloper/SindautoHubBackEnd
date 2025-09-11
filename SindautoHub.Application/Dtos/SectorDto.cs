using System;

namespace SindautoHub.Application.Dtos.SectorDtos;

public class CreateSectorRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string OpeningsHours { get; set; }
}

public class UpdateSectorRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? OpeningsHours { get; set; }
}

public class SectorResponse
{
    public Guid Id { get; set; }
    public string NameSector { get; set; }
    public string Description { get; set; }
    public string OpeningsHours { get; set; }
    public int MembersCount { get; set; } 

    }

