using System.Net;
using System.Net.Sockets;
using System.Text;
using Server.Controller;
using Server.Handler;
using Server.Routing;
using Shared.Networking.Http;

namespace Server;

public class MtcgServer
{
    private readonly int _port;
    private RequestHandler _requestHandler;
    public static Router Router { get; private set; } = null!;

    private static Dictionary<string, string> _registeredTokens = new();

    public MtcgServer(int port = 8080)
    {
        _requestHandler = new RequestHandler();
        Router = new Router();
        _port = port;

        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        Router.RegisterRoute(HttpMethod.Get, "/test", async request =>
        {
            await Task.CompletedTask;
            return HttpUtilities.GenerateResponse(HttpStatusCode.OK, "test");
        });
        
        Router.RegisterRoute(HttpMethod.Post, "/register", UserController.RegisterUserAsync);
        Router.RegisterRoute(HttpMethod.Post, "/login", UserController.LoginUserAsync);
        Router.RegisterRoute(HttpMethod.Patch, "/user/username", UserController.UpdateUsernameAsync);
        
        Router.RegisterRoute(HttpMethod.Post, "/packs/open", CardController.OpenCardPackAsync);
        Router.RegisterRoute(HttpMethod.Put, "/user/deck/update", CardController.UpdateUserDeckAsync);
        
        Router.RegisterRoute(HttpMethod.Post, "/battle/start", BattleController.StartBattleAsync);
    }

    public void Start()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, _port);
        listener.Start();
        Console.WriteLine($"Server started, listening on {IPAddress.Any}:{_port}");

        while (true)
        {
            //TcpClient client = await listener.AcceptTcpClientAsync();
            TcpClient client = listener.AcceptTcpClient();
            Task.Run(() => HandleClientAsync(client));
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        using (client)
        {
            using NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[client.ReceiveBufferSize];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

            if (bytesRead == 0)
                return;

            string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received request:\n{request}");

            HttpResponseMessage response = await _requestHandler.HandleRequestAsync(HttpUtilities.ParseHttpRequest(request));
            Console.WriteLine(HttpUtilities.HttpResponseMessageToString(response));
            byte[] responseBuffer = Encoding.UTF8.GetBytes(HttpUtilities.HttpResponseMessageToString(response));
            await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);
            await stream.FlushAsync();
        }
    }
}