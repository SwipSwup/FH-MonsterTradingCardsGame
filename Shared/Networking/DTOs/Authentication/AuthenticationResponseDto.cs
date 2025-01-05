namespace Shared.Networking.DTOs.Authentication;

public class AuthenticationResponseDto : IResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    
    public string Token { get; set; }
}