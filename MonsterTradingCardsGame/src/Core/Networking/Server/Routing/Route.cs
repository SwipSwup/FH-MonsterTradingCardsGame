namespace MonsterTradingCardsGame.Core.Networking.Server.Routing;

public struct Route
{
    public string Path { get; private set; }
    
    public Routing.HttpRouter.RouteHandler Handler { get; private set; }
    //public Func<HttpRequest, string> Handler { get; private set; }

    public Route(string path, Routing.HttpRouter.RouteHandler handler)
    {
        Path = path;
        Handler = handler;
    }
}