using System;
using System.Threading.Tasks;
using MonsterTradingCardsGame.Core.Networking.Http;

namespace MonsterTradingCardsGame.Core.Networking.Server.Router;

public struct Route
{
    public string Path { get; private set; }
    
    public Router.RouteHandler Handler { get; private set; }
    //public Func<HttpRequest, string> Handler { get; private set; }

    public Route(string path, Router.RouteHandler handler)
    {
        Path = path;
        Handler = handler;
    }
}