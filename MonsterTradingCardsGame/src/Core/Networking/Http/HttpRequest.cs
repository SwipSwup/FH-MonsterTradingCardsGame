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

    public bool HasParameters(params string[] parameters)
    {
        foreach (string parameter in parameters)
        {
            if(!QueryParameters.ContainsKey(parameter)) 
                return false;
        }
        return true;
    }
}