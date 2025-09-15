using System;

namespace SindautoHub.Application.Dtos.PositionDtos;

public class CreatePositionRequest
{
    public string PositionName { get; set; }
    public string? DescriptionDuties { get; set; }
}

public class UpdatePositionRequest
{
    public string? PositionName { get; set; }
    public string? DescriptionDuties { get; set; }
}

public class PositionResponse
{
    public Guid Id { get; set; }
    public string? PositionName { get; set; }
    public string? DescriptionDuties { get; set; }
}
