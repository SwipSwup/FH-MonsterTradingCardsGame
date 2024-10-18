using System.Net;
using MonsterTradingCardsGame.Networking.Http;
using MonsterTradingCardsGame.Networking.Server.Routing;

namespace MonsterTradingCardsGame.Networking.Server.Handler;


public class RequestHandler
{
    
    public async Task<string> HandleRequestAsync(HttpRequest? request)
    {
        if (request == null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "Bad Request", "The request could not be parsed.");
        }

        if (Server.HttpRouter.TryGetHandler(request.Value.Method, request.Value.Path, out HttpRouter.RouteHandler? handler))
        {
            try
            {
                return await handler?.Invoke((HttpRequest)request)!;
            }
            catch (Exception exception)
            {
                return HttpUtilities.GenerateErrorResponse(HttpStatusCode.InternalServerError, "Internal Server Error", $"{handler.Method.DeclaringType}: {exception.Message}" );
            }
        }

        return HttpUtilities.GenerateErrorResponse(HttpStatusCode.NotFound, "Not Found", "The requested resource was not found.");
    }
}