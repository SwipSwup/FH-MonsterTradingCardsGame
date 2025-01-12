using Shared.Networking.DTOs;

namespace Shared.DTOs.Authentication;

public class AuthenticationResponseDto : IResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    
    public string Token { get; set; }
}