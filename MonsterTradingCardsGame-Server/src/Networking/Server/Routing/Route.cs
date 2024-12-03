namespace MonsterTradingCardsGame.Networking.Server.Routing;

public struct Route
{
    public string Path { get; private set; }
    
    public HttpRouter.RouteHandler? Handler { get; private set; }
    //public Func<HttpRequest, string> Handler { get; private set; }

    public Route(string path, HttpRouter.RouteHandler? handler)
    {
        Path = path;
        Handler = handler;
    }
}