using System.ComponentModel.DataAnnotations;
using AnimeCalendar.Data;

namespace AnimeCalendar.Work;

public class AnimeSandbox : Anime
{

    [Required] public sealed override string Name { get; set; }
    [Required] public sealed override string Cover { get; set; }
    [Required] public sealed override string Banner { get; set; }
    [Required] public sealed override int NumberEpisodes { get; set; }
    [Required] public sealed override DateTime StartDate { get; set; }

    protected Anime Target { get; }

    public AnimeSandbox(Anime target)
    {
        Target = target;
        Name = target.Name;
        Cover = target.Cover;
        Banner = target.Banner;
        StartDate = target.StartDate;
        NumberEpisodes = target.NumberEpisodes;
    }

    public AnimeSandbox(string name, string? cover, string? banner, DateTime? startDate, int? numberEpisodes)
    {
        Name = name;
        Cover = cover ?? "";
        Banner = banner ?? "";
        StartDate = startDate ?? DateTime.Now;
        NumberEpisodes = numberEpisodes ?? 12;
        Target = new Anime();
    }

    public virtual void Save(ApplicationDbContext dbContext)
    {
        CopyTo(Target);
    }

    public void CopyTo(Anime target)
    {
        target.Name = Name;
        target.Cover = Cover;
        target.Banner = Banner;
        target.StartDate = StartDate;
        target.NumberEpisodes = NumberEpisodes;
    }
}