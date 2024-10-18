namespace MonsterTradingCardsGame.Networking.Http;

public struct HttpRequest
{
    public System.Net.Http.HttpMethod Method { get; set; }
    public string Path { get; set; }
    public Dictionary<string, string> QueryParameters { get; set; }
    public Dictionary<string, string?> Headers { get; set; }
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