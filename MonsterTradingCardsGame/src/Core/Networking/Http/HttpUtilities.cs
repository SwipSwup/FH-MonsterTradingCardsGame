using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MonsterTradingCardsGame.Core.Networking.Http;

public static class HttpUtilities
{
    public static Dictionary<string, string> ExtractQueryParameters(string query)
    {
        Dictionary<string, string> parameters = new();
        if (string.IsNullOrEmpty(query)) return parameters;

        string[] pairs = query.Split('&');
        foreach (string pair in pairs)
        {
            string[] keyValue = pair.Split('=');
            if (keyValue.Length == 2)
            {
                parameters[WebUtility.UrlDecode(keyValue[0])] = WebUtility.UrlDecode(keyValue[1]);
            }
        }

        return parameters;
    }

    public static HttpRequest? ParseRequest(string request)
    {
        try
        {
            string[] lines = request.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string[] requestLine = lines[0].Split(' ');
            string url = requestLine[1];
            string query = url.Contains("?") ? url.Split('?')[1] : string.Empty;
            int emptyLineIndex = Array.IndexOf(lines, "");

            return new HttpRequest
            {
                Method = (HttpMethod) Enum.Parse(typeof(HttpMethod), requestLine[0], true),
                Path = url.Contains("?") ? url.Split('?')[0] : url,
                QueryParameters = ExtractQueryParameters(query),
                Body = emptyLineIndex >= 0 && emptyLineIndex + 1 < lines.Length
                    ? string.Join("\r\n", lines, emptyLineIndex + 1, lines.Length - emptyLineIndex - 1)
                    : string.Empty
            };
        }
        catch
        {
            return null;
        }
    }

    public static string GenerateErrorResponse(HttpStatusCode statusCode, string error, string message)
    {
        return GenerateResponse(statusCode, $"{{\"error\": \"{error}\", \"message\": \"{message}\"}}");
    }
    
    public static string GenerateResponse(HttpStatusCode statusCode, string content)
    {
        return $"HTTP/1.1 {(int)statusCode} {statusCode}\r\n" +
               $"Content-Type: application/json\r\n" +
               $"Content-Length: {Encoding.UTF8.GetByteCount(content)}\r\n" +
               $"Connection: close\r\n\r\n" +
               content;
    }
}