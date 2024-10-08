namespace MonsterTradingCardsGame.Core.Networking.DTOs.Authentication;

public struct AuthenticationResponse : IResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }

    public string Token { get; set; }
}