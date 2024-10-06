using System.Net;
using System.Threading.Tasks;
using MonsterTradingCardsGame.Core.Networking.Http;

namespace MonsterTradingCardsGame.Core.Networking.Server.Handler;

public class UserController
{
    public async Task<string> HandleLogin(HttpRequest request)
    {
        if (!request.HasParameters("username", "password"))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "BadRequest", "Missing username and/or password");
        }
        
        //DB call
        await Task.CompletedTask;
        
    }
}