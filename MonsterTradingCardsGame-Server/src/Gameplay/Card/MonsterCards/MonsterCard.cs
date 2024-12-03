namespace MonsterTradingCardsGame.Gameplay.Card.MonsterCards;

public class MonsterCard : Card
{
    public Species Species { get; private set; }

    public MonsterCard(string name, float damage, Element element, Species species) : base(name, damage, element)
    {
        Species = species;
    }

    public override float CalculateDamage(Card card)
    {
        if (card is MonsterCard monsterCard)
            return DamageModifiers.TryGetValue(monsterCard.Species, out float modifier) ? Damage * modifier : Damage;
        
        return Damage;
    }
}