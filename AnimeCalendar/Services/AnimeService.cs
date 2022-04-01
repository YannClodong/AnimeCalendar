using System.Security.Claims;
using AnimeCalendar.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace AnimeCalendar.Services;

public class AnimeService
{
    private readonly ApplicationDbContext _db;
    private readonly HttpContextAccessor _httpContextAccessor;
    private readonly NavigationManager _navigationManager;

    public AnimeService(ApplicationDbContext db, HttpContextAccessor httpContextAccessor, NavigationManager navigationManager)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _navigationManager = navigationManager;
    }


    public (Anime anime, List<(Collection collection, bool contains)> collections) GetAnimeWithUserCollection(long animeId)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var anime = _db.Animes.SingleOrDefault(a => a.Id == animeId);

        if (anime == null)
        {
            _navigationManager.NavigateTo("/");
            throw new Exception("Anime not found.");
        }


        var collections = _db.Collections
            .Include(c => c.Elements)
            .Where(c => c.UserId == userId);

        var aggregatedCollection = collections
            .ToArray()
            .Select(c => (c, c.Elements.Any(e => e.AnimeId == animeId)))
            .ToList();
        
        return (anime, aggregatedCollection);
    }

    public List<Anime> GetAll()
    {
        return _db.Animes.ToArray().OrderBy(a => a.GetNext()).ToList();
    }

    public List<Anime> AiringAnimes()
    {
        return _db.Animes
            .ToArray()
            .Where(a => a.StartDate + a.NumberEpisodes * TimeSpan.FromDays(7) > DateTime.Now - TimeSpan.FromDays(1))
            .ToList();
    }
}