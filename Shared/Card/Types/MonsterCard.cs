namespace Shared.Card.Types;

public abstract class MonsterCard(string name, float damage, Element element, Species species)
    : Card(name, damage, element)
{
    public Species Species { get; private set; } = species;

    public override float CalculateDamage(Card card)
    {
        if (card is MonsterCard monsterCard)
            return DamageModifiers.TryGetValue(monsterCard.Species, out float modifier) ? Damage * modifier : Damage;
        
        return Damage;
    }
}