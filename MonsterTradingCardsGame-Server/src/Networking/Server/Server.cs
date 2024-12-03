using System.Net;
using System.Net.Sockets;
using System.Text;
using MonsterTradingCardsGame.Networking.Http;
using MonsterTradingCardsGame.Networking.Server.Controller;
using MonsterTradingCardsGame.Networking.Server.Handler;
using MonsterTradingCardsGame.Networking.Server.Routing;
using HttpMethod = System.Net.Http.HttpMethod;

namespace MonsterTradingCardsGame.Networking.Server;

public class Server
{
    private readonly int _port;
    private RequestHandler _requestHandler;

    public static HttpRouter HttpRouter { get; private set; }

    private static Dictionary<string, string> _registeredTokens = new();

    public Server(int port = 8080)
    {
        _requestHandler = new RequestHandler();
        HttpRouter = new HttpRouter();
        _port = port;

        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        HttpRouter.RegisterRoute(HttpMethod.Get, "/test", async request =>
        {
            await Task.CompletedTask;
            return HttpUtilities.GenerateResponse(HttpStatusCode.OK, "test");
        });
        
        HttpRouter.RegisterRoute(HttpMethod.Post, "/register", UserController.RegisterUserAsync);
        HttpRouter.RegisterRoute(HttpMethod.Post, "/login", UserController.LoginUserAsync);
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