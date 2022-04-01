
using AnimeCalendar.Work;
using Microsoft.AspNetCore.Components;

namespace AnimeCalendar.Services;

public class RouteService
{
    private readonly NavigationManager _navigationManager;
    private Dictionary<string, MyRoute> _cache = new();

    public RouteService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public string Current => _navigationManager.ToBaseRelativePath(_navigationManager.Uri);

    private MyRoute CreateRoute(string route)
    {
        if (_cache.TryGetValue(route, out var r)) return r;
        r = new MyRoute(route, _navigationManager, this);
        _cache.Add(route, r);
        return r;
    }

    public bool IsCurrent(MyRoute route) => route.Route == "/" + Current;

    public MyRoute GetHome()
    {
        return CreateRoute("/");
    }
    
    public MyRoute GetAnime(long animeId)
    {
        return CreateRoute("/animes/" + animeId);
    }

    public MyRoute GetAnimeEdit(long animeId)
    {
        return CreateRoute($"/animes/{animeId}/edit");
    }

    public MyRoute GetCreateAnime()
    {
        return CreateRoute("/animes/create");
    }
    
    public MyRoute GetAnimeList()
    {
        return CreateRoute("/animes");
    }
    

    public MyRoute GetCollection(long collectionId)
    {
        return CreateRoute($"/collections/{collectionId}");
    }

    public MyRoute GetCollectionEdit(long collectionId)
    {
        return CreateRoute($"/collections/{collectionId}/edit");
    }

    public MyRoute GetCollectionCreate()
    {
        return CreateRoute($"/collections/create");
    }

    public MyRoute GetCollectionCalendar(long collectionId)
    {
        return CreateRoute($"/collections/{collectionId}/calendar");
    }


    public MyRoute GetCollectionsList()
    {
        return CreateRoute($"/collections");
    }

    public MyRoute GetLogout()
    {
        return CreateRoute("/Account/Identity/Logout");
    }

    public MyRoute GetLogin()
    {
        return CreateRoute("/Account/Identity/Login");
    }

    public MyRoute GetRegister()
    {
        return CreateRoute("/Account/Identity/Register");
    }

    public MyRoute GetFullCalendar()
    {
        return CreateRoute("/calendar");
    }
}