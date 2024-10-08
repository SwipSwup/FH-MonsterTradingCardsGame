using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace MonsterTradingCardsGame.Core.Networking.Http;

public struct HttpRequest
{
    public HttpMethod Method { get; set; }
    public string Path { get; set; }
    public Dictionary<string, string> QueryParameters { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public MemoryStream Body { get; set; }
    
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