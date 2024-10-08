using System;
using System.Net;
using System.Threading.Tasks;
using MonsterTradingCardsGame.Core.Networking.Http;

namespace MonsterTradingCardsGame.Core.Networking.Server.Handler;

public class RequestHandler
{
    
    public async Task<string> HandleRequestAsync(HttpRequest? request)
    {
        if (request == null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "Bad Request", "The request could not be parsed.");
        }

        if (Server.Router.TryGetHandler(request.Value.Method, request.Value.Path, out var handler))
        {
            try
            {
                return await handler((HttpRequest)request);
            }
            catch (Exception ex)
            {
                return HttpUtilities.GenerateErrorResponse(HttpStatusCode.InternalServerError, "Internal Server Error", $"{handler.Method.DeclaringType}: {ex.Message}" );
            }
        }

        return HttpUtilities.GenerateErrorResponse(HttpStatusCode.NotFound, "Not Found", "The requested resource was not found.");
    }
}