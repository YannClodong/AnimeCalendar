namespace AnimeCalendar.Data;

public class InCollection
{
    public long Id { get; set; }
    
    public long AnimeId { get; set; }
    public Anime Anime { get; set; }
    
    public long CollectionId { get; set; }
    public Collection Collection { get; set; }
    
    public DateTime CreatedAt { get; set; }
}