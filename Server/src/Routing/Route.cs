namespace Server.Routing;

public struct Route
{
    public string Path { get; private set; }
    
    public RouteHandler Handler { get; private set; }

    public Route(string path, RouteHandler handler)
    {
        Path = path;
        Handler = handler;
    }
}