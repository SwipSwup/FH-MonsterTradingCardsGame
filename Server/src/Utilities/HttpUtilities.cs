using System.Net;
using System.Text;

namespace Shared.Networking.Http;

public static class HttpUtilities
{
    public static string HttpResponseMessageToString(HttpResponseMessage response)
    {
        StringBuilder rawResponse = new StringBuilder();

        rawResponse.AppendLine(
            $"HTTP/{response.Version.Major}.{response.Version.Minor} {(int)response.StatusCode} {response.ReasonPhrase}");

        foreach (KeyValuePair<string, IEnumerable<string>> header in response.Headers)
        {
            rawResponse.AppendLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }

        foreach (KeyValuePair<string, IEnumerable<string>> header in response.Content.Headers)
        {
            rawResponse.AppendLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }

        rawResponse.AppendLine();

        rawResponse.Append(response.Content.ReadAsStringAsync().Result);

        return rawResponse.ToString();
    }

    public static HttpRequestMessage? ParseHttpRequest(string requestString)
    {
        try
        {
            string[] lines = requestString.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            string[] requestLine = lines[0].Split(' ');

            HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod(requestLine[0]), requestLine[1]);
            requestMessage.Version = new Version(requestLine[2].Split("/")[1]);

            for (int i = 1; i < lines.Length; i++)
            {
                string headerLine = lines[i];
                string[] headerToken = headerLine.Split(": ", 2);

                if (headerToken.Length == 2)
                    requestMessage.Headers.TryAddWithoutValidation(headerToken[0], headerToken[1]);
            }

            int bodyIndex = requestString.IndexOf("\r\n\r\n", StringComparison.Ordinal) + 4;
            if (bodyIndex > 4)
            {
                string bodyContent = requestString.Substring(bodyIndex);
                requestMessage.Content = new StringContent(bodyContent, Encoding.UTF8, "application/json");
            }

            return requestMessage;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return null;
        }
    }


    public static HttpResponseMessage GenerateErrorResponse(HttpStatusCode statusCode, string message)
    {
        return GenerateResponse(statusCode, $"{{\"error\": \"{statusCode.ToString()}\", \"message\": \"{message}\"}}");
    }

    public static HttpResponseMessage GenerateResponse(HttpStatusCode statusCode, string content)
    {
        return new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };
    }
}