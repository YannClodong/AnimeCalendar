namespace AnimeCalendar.Data;

public class Anime
{
    public virtual long Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Cover { get; set; }
    public virtual string Banner { get; set; }
    
    public virtual DateTime StartDate { get; set; }
    public virtual int NumberEpisodes { get; set; }


    public List<InCollection> Collections { get; set; } = null!;

    public DateTime GetNext()
    {
        if(StartDate + (NumberEpisodes - 1) * TimeSpan.FromDays(7) < DateTime.Now)
            return DateTime.MaxValue;

        if (StartDate > DateTime.Now)
            return StartDate;
        
        var nWeek = (int)((DateTime.Now.Date - StartDate + TimeSpan.FromHours(5)) / TimeSpan.FromDays(7));
        return StartDate + (nWeek) * TimeSpan.FromDays(7);
    }
}