using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MonsterTradingCardsGame.Core.Networking.Http;
using MonsterTradingCardsGame.Core.Networking.Server.Handler;

namespace MonsterTradingCardsGame.Core.Networking.Server;

public class Server
{
    private readonly int _port;
    private RequestHandler _requestHandler;

    public static Router.Router Router { get; private set; }

    private static Dictionary<string, string> _registeredTokens = new();

    public Server(int port = 8080)
    {
        _requestHandler = new RequestHandler();
        Router = new Router.Router();
        _port = port;

        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        Router.RegisterRoute(HttpMethod.GET, "/test", async request =>
        {
            await Task.CompletedTask;
            return HttpUtilities.GenerateResponse(HttpStatusCode.OK, "test");
        });
        
        Router.RegisterRoute(HttpMethod.POST, "/register", UserController.RegisterUserAsync);
        Router.RegisterRoute(HttpMethod.POST, "/login", UserController.LoginUserAsync);
        
    }

    public void Start()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, _port);
        listener.Start();
        Console.WriteLine($"Server started, listening on port {_port}...");

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

            string response = await _requestHandler.HandleRequestAsync(HttpUtilities.ParseRequest(request));
            //Console.WriteLine(response);
            byte[] responseBuffer = Encoding.UTF8.GetBytes(response);
            await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);
            await stream.FlushAsync();
        }
    }
}