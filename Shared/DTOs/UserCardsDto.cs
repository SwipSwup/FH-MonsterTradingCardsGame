namespace Shared.DTOs;

public class UserCardsDto
{
    public int UserId { get; set; } 
    
    public int CardId { get; set; }
    
    public bool IsInDeck { get; set; }
    
    public bool IsInTrade { get; set; }

}