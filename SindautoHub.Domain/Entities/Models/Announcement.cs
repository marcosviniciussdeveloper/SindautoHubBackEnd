using System;

namespace SindautoHub.Domain.Entities;

public class Announcement
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }

    public Guid PostedById { get; set; }

    public Guid AuthorId { get; set; }
    public User Author { get; set; }
}