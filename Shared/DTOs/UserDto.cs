namespace Shared.DTOs;

public class UserDto
{
    public int UserId;

    public string Username { get; set; }

    public string Password { get; set; }

    public int Mmr;

    public int Currency;
}