using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonsterTradingCardsGame.Core.Networking.Http;

namespace MonsterTradingCardsGame.Core.Networking.Server.Router;

public class Router
{
    public delegate Task<string> RouteHandler(HttpRequest request);

    private readonly Dictionary<HttpMethod, List<Route>> _routes = new();

    public Router()
    {
    }

    public void RegisterRoute(HttpMethod httpMethod, string path, RouteHandler handler)
    {
        if (_routes.TryGetValue(httpMethod, out List<Route> r))
            r.Add(new Route(path, handler));
        else
            _routes.Add(httpMethod, new List<Route> {new(path, handler)});
    }

    public bool TryGetHandler(HttpMethod method, string path, out RouteHandler handler)
    {
        if (_routes.TryGetValue(method, out List<Route> r))
        {
            foreach (Route route in r)
            {
                if (route.Path == path)
                {
                    handler = route.Handler;
                    return true;
                }
            }
        }

        handler = null;
        return false;
    }
}