using System.Security.Claims;
using AnimeCalendar.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeCalendar.Services;

public class CollectionService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly HttpContextAccessor _httpContextAccessor;

    public CollectionService(ApplicationDbContext dbContext, HttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    
    /// <summary>
    /// Return the list of the user's collections if he is logged, an empty list otherwise
    /// </summary>
    /// <returns></returns>
    public List<Collection> GetUserCollectionsIfLogged()
    {
        if (_httpContextAccessor.HttpContext == null)
            return new List<Collection>();

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return new List<Collection>();
        
        return _dbContext.Users
            .Include(u => u.Collections)
            .SingleOrDefault(u => u.Id == userId)!
            .Collections
            .ToList();
    }

    public void CreateCollection(string name, List<Anime>? animes)
    {
        var toAdd = animes ?? new List<Anime>();
        
        if (_httpContextAccessor.HttpContext == null)
            throw new Exception("Not logged !");

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var collection = new Collection()
        {
            Name = name,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
            Elements = toAdd.Select(a => new InCollection()
            {
                AnimeId = a.Id,
                CreatedAt = DateTime.Now.ToUniversalTime()
            }).ToList(),
            UserId = userId
        };
        
        _dbContext.Collections.Add(collection);
        _dbContext.SaveChanges();
    }

    public List<Collection> GetCollections()
    {
        if (_httpContextAccessor.HttpContext == null)
            return new List<Collection>();

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var query = _dbContext.Collections
            .Include(c => c.Elements)
            .ThenInclude(c => c.Anime)
            .Where(c => c.UserId == userId);

        return query.ToList();
    }

    public Collection GetCollection(long collectionId)
    {
        if (_httpContextAccessor.HttpContext == null)
            throw new Exception("Not logged !");

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        return _dbContext.Collections
                             .Include(c => c.Elements)
                             .ThenInclude(c => c.Anime)
                             .SingleOrDefault(c => c.Id == collectionId && c.UserId == userId)
                         ?? throw new Exception("Collection not found !");
    }

    public void Destroy(long collectionId)
    {
        if (_httpContextAccessor.HttpContext == null)
            throw new Exception("Not logged !");
        
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) throw new Exception("Not logged !");
        
        var collection = _dbContext.Collections
                             .Include(c => c.Elements)
                             .Single(c => c.Id == collectionId)
                         ?? throw  new Exception("Collection not found.");
        
        if (collection.UserId != userId) throw new Exception("You don't owe this collection.");

        _dbContext.Collections.Remove(collection);
        _dbContext.SaveChanges();
    }
    public void AddAnime(Collection collection, Anime anime)
    {
        if (_httpContextAccessor.HttpContext == null)
            throw new Exception("Not logged !");

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        if (userId != collection.UserId)
            throw new Exception("You must own the collection to add elements");
        
        collection.Elements.Add(new InCollection()
        {
            AnimeId = anime.Id,
            CreatedAt = DateTime.Now.ToUniversalTime()
        });
        _dbContext.SaveChanges();
    }
    public void RemoveAnime(Collection collection, Anime anime)
    {
        if (_httpContextAccessor.HttpContext == null)
            throw new Exception("Not logged !");

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        if (userId != collection.UserId)
            throw new Exception("You must own the collection to add elements");

        var predicate = (InCollection c) => c.CollectionId == collection.Id && c.AnimeId == anime.Id;

        if (!_dbContext.InCollections.Any(predicate))
            throw new Exception("The anime is not in this collection.");
        
        _dbContext.InCollections.Remove(_dbContext.InCollections.Single(predicate));
        _dbContext.SaveChanges();
    }
}