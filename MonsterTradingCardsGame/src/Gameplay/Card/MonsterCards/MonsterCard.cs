using MonsterTradingCardsGame.Gameplay.Card.SpellCards;

namespace MonsterTradingCardsGame.Gameplay.Card.MonsterCards;

public class MonsterCard : Card
{
    public Specie Specie { get; }

    public MonsterCard(string name, float damage, Element element, Specie specie) : base(name, damage, element)
    {
        Specie = specie;
    }

    public override float CalculateDamage(Card card)
    {
        if (card is MonsterCard monsterCard)
        {
            return DamageModifiers.TryGetValue(monsterCard.Specie, out var modifier) ? Damage * modifier : Damage;
        }
        
        return Damage;
    }
}