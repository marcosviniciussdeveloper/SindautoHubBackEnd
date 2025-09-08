using System;

namespace SindautoHub.Application.Dtos.PositionDtos;

public class CreatePositionRequest
{
    public string Name { get; set; }
    public string? DescriptionDuties { get; set; }
}

public class UpdatePositionRequest
{
    public string? Name { get; set; }
    public string? DescriptionDuties { get; set; }
}

public class PositionResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? DescriptionDuties { get; set; }
}
