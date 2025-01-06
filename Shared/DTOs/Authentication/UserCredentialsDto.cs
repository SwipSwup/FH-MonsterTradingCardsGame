namespace Shared.Networking.DTOs.Authentication;

public class UserCredentialsDto
{
    public Guid UserId { get; set; }
    
    public string Username { get;  set; }
    
    public string Password { get;  set; }
}