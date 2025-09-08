// --- AnnouncementDto.cs ---
namespace SindautoHub.Application.Dtos.AnnouncementDtos;

public class CreateAnnouncementRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime? ExpiresAt { get; set; }

}

public class UpdateAnnouncementRequest
{
    public Guid Id { get; set; }
    public string Authotname { get; set; }

  

    public string? Tittle { get; set; }
    
    public string? Content { get; set; }
}


public class AnnouncementResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid PostedById { get; set; }
    public string AuthorName { get; set; }
}