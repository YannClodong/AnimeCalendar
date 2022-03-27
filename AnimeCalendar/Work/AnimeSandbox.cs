using System.ComponentModel.DataAnnotations;
using AnimeCalendar.Data;

namespace AnimeCalendar.Work;

public class AnimeSandbox : Anime
{

    [Required] public sealed override string Name { get; set; }
    [Required] public sealed override string Cover { get; set; }
    [Required] public sealed override string Banner { get; set; }
    [Required] public sealed override int NumberEpisodes { get; set; } = 13;
    [Required] public sealed override DateTime StartDate { get; set; } = DateTime.Now;

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

    public virtual void Save(ApplicationDbContext dbContext)
    {
        Target.Name = Name;
        Target.Cover = Cover;
        Target.Banner = Banner;
        Target.StartDate = StartDate;
        Target.NumberEpisodes = NumberEpisodes;
    }
}