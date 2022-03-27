using Microsoft.AspNetCore.Identity;

namespace AnimeCalendar.Data;

public class Collection
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public User User { get; set; } = null!;
    public string UserId { get; set; } = null!;

    public List<InCollection> Elements { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}