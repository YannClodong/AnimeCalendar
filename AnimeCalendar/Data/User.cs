using Microsoft.AspNetCore.Identity;

namespace AnimeCalendar.Data;

public class User : IdentityUser
{
    public List<Collection> Collections { get; set; } = null!;
}