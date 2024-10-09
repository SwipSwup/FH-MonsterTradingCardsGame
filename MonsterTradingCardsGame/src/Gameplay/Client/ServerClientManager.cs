using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MonsterTradingCardsGame.Core.Networking.Http;

namespace MonsterTradingCardsGame.Gameplay.Client;

public class ServerClientManager
{
    private static int _port = 8080;
    private static string _host = "http://localhost";

    public static string Token = "not_set";

    private static HttpClient GetClient()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");

        client.BaseAddress = new Uri($"{_host}:{_port}");

        return client;
    }

    public static async Task<HttpResponseMessage> SendGetRequest(string url)
    {
        using HttpClient client = GetClient();
        HttpResponseMessage response = await client.GetAsync(url);
        return response;
    }

    public static async Task<HttpResponseMessage> SendPostRequest(string url, string jsonData)
    {
        using HttpClient client = GetClient();
        client.DefaultRequestHeaders.Add(HttpHeader.ContentType, "application/json");
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(url, content);
        return response;
    }

    public static async Task<HttpResponseMessage> SendPutRequest(string url, string jsonData)
    {
        using HttpClient client = GetClient();
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PutAsync(url, content);
        return response;
    }

    public static async Task<HttpResponseMessage> SendDeleteRequest(string url)
    {
        using HttpClient client = GetClient();
        HttpResponseMessage response = await client.DeleteAsync(url);
        return response;
    }
}