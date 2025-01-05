using System.Net;
using Server.Routing;
using Shared.Networking.Http;

namespace Server.Handler;

public class RequestHandler
{
    
    public async Task<HttpResponseMessage> HandleRequestAsync(HttpRequestMessage? request)
    {
        if (request == null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "The request could not be parsed.");
        }

        if (MtcgServer.Router.TryGetHandler(request.Method, request.RequestUri!.ToString(), out RouteHandler? handler))
        {
            try
            {
                if (handler == null)
                {
                    return HttpUtilities.GenerateErrorResponse(HttpStatusCode.NotFound, "The given path could not be found.");
                }
                
                return await handler.Invoke(request);
            }
            catch (Exception exception)
            {

                return HttpUtilities.GenerateErrorResponse(HttpStatusCode.InternalServerError, $"{handler.Method.DeclaringType}: {exception.Message}" );
            }
        }

        return HttpUtilities.GenerateErrorResponse(HttpStatusCode.NotFound, "The requested resource was not found.");
    }
}