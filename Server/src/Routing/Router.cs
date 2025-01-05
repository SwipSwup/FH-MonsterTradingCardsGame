namespace Server.Routing
{
    public delegate Task<HttpResponseMessage> RouteHandler(HttpRequestMessage request);

    public class Router
    {
        private readonly Dictionary<HttpMethod, List<Route>> _routes = new();

        public void RegisterRoute(HttpMethod httpMethod, string path, RouteHandler handler)
        {
            if (_routes.TryGetValue(httpMethod, out List<Route>? route))
                route.Add(new Route(path, handler));
            else
                _routes.Add(httpMethod, [new Route(path, handler)]);
        }

        public bool TryGetHandler(HttpMethod method, string path, out RouteHandler? handler)
        {
            if (_routes.TryGetValue(method, out List<Route>? route))
            {
                handler = route.Find(r => r.Path == path).Handler;
                
                if (handler != default)
                    return true;
            }

            handler = null;
            return false;
        }
    }
}