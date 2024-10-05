using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonsterTradingCardsGame.Core.Networking.Server.Router;

public class Route
{
    public string Path { get; private set; }
    
    public Router.RouterHandler Handler { get; private set; }

    public Route(string path, Router.RouterHandler handler)
    {
        Path = path;
        Handler = handler;
    }
}