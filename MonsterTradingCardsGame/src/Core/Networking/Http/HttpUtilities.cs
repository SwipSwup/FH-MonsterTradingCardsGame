using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            string[] lines = request.Split(new[] { "\r\n" }, StringSplitOptions.None);
            string[] requestLine = lines[0].Split(' ');

            Dictionary<string, string> headers = new();
            int bodyIndex = 0;
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    bodyIndex = i + 1;
                    break;
                }

                string[] headerParts = lines[i].Split(new[] { ": " }, StringSplitOptions.None);
                if (headerParts.Length == 2)
                {
                    headers[headerParts[0]] = headerParts[1];
                }
            }

            return new HttpRequest
            {
                Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), requestLine[0], true),
                Path = requestLine[1].Split('?')[0],
                Headers = headers,
                Body = new MemoryStream(Encoding.UTF8.GetBytes(bodyIndex < lines.Length ? string.Join("\r\n", lines.Skip(bodyIndex)) : string.Empty)),
                QueryParameters = ExtractQueryParameters(requestLine[1])
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
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