namespace Shared.DTOs;

public class UserDto
{
    public Guid UserId;
    public string Username { get; set; }
    
    public List<CardDto> AllCards { get; set; }
    public List<CardDto> DeckCards { get; set; }
}