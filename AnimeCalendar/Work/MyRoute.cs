using AnimeCalendar.Services;
using Microsoft.AspNetCore.Components;

namespace AnimeCalendar.Work;

public class MyRoute
{
    public string Route { get; }
    private readonly NavigationManager _navigationManager;
    private readonly RouteService _routeService;

    public MyRoute(string route, NavigationManager navigationManager, RouteService routeService)
    {
        Route = route;
        _navigationManager = navigationManager;
        _routeService = routeService;
    }

    public bool IsCurrent => _routeService.IsCurrent(this);
    public void Navigate()
    {
        _navigationManager.NavigateTo(Route);
    }

    public static implicit operator string(MyRoute myRoute)
    {
        return myRoute.ToString();
    }
    
    public override string ToString()
    {
        return Route;
    }
}