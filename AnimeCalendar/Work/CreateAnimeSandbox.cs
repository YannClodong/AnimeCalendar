using AnimeCalendar.Data;

namespace AnimeCalendar.Work;

public class CreateAnimeSandbox : AnimeSandbox
{
    public CreateAnimeSandbox() : base(new Anime())
    {
        StartDate = DateTime.Now;
        NumberEpisodes = 13;
    }

    public override void Save(ApplicationDbContext dbContext)
    {
        base.Save(dbContext);
        dbContext.Animes.Add(Target);
    }
}