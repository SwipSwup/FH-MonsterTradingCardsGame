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

namespace MonsterTradingCardsGame.Core.Networking.Server;

public class Server
{
    private readonly int _port;
    private RequestHandler.RequestHandler _requestHandler;

    public static Router.Router Router { get; private set; }

    private static Dictionary<string, string> _registeredTokens = new();

    public Server(int port = 8080)
    {
        _requestHandler = new RequestHandler.RequestHandler();
        Router = new Router.Router();
        _port = port;

        Router.RegisterRoute(HttpMethod.GET, "/test", async request =>
        {
            await Task.CompletedTask;
            return HttpUtilities.GenerateResponse(HttpStatusCode.OK, "test");
        });
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

    /*private void HandleClient(TcpClient client)
    {
        using (client)
        {
            using NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[client.ReceiveBufferSize];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            if (bytesRead == 0)
                return;

            string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received request:\n{request}");

            string response = _requestHandler.HandleRequest(HttpUtilities.ParseRequest(request));
            Console.WriteLine(response);
            byte[] responseBuffer = Encoding.UTF8.GetBytes(response);
            stream.Write(responseBuffer, 0, responseBuffer.Length);
        }
    }*/

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
            //Console.WriteLine($"Received request:\n{request}");

            string response = await _requestHandler.HandleRequestAsync(HttpUtilities.ParseRequest(request));
            Console.WriteLine(response);
            byte[] responseBuffer = Encoding.UTF8.GetBytes(response);
            await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);
            await stream.FlushAsync();
        }
    }

    public static string GenerateToken(string username, string userId)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("z97ki8G7ueDqICty6OxMHsL29tq3w6rq06FWsJt1o7lLGU4X6qyFvjdvkL1rGLjd"));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: "MTCG",
            audience: "MTCG",
            claims: new Claim[] { 
                new(JwtRegisteredClaimNames.Sub, username),
                new("userId", userId),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            },
            signingCredentials: credentials
        );

        string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        
        _registeredTokens.Add(username, token);
        
        return token;
    }
}