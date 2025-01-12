using Shared.Card;

namespace Shared.DTOs;

public class CardDto
{
    public int CardId { get; set; }
    
    public string CardName { get; set; } = string.Empty;
    
    public Species? Species { get; set; }
    
    public float Damage { get; set; }
    
    
    public Rarity Rarity { get; set; }
    
    public Element Element { get; set; }
    
    public CardType CardType { get; set; }
}