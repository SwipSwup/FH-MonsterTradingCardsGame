namespace Shared.DTOs;

public class BattleRoundDto
{
    public int BattleId { get; set; }
    
    public int RoundNumber { get; set; }
    
    public int Card1Id { get; set; }
    
    public int Card2Id { get; set; }
    
    public int WinnerCardId { get; set; }
    
    public string Result { get; set; }
}