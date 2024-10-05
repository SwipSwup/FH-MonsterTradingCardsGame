using System;
using System.Collections.Generic;
using System.Net;

namespace MonsterTradingCardsGame.Core.Networking.Http;

public struct HttpRequest
{
    public HttpMethod Method { get; set; }
    public string Path { get; set; }
    public Dictionary<string, string> QueryParameters { get; set; }
    public string Body { get; set; }
}