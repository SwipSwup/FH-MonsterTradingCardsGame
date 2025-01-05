namespace Shared.Card.Types;

public class SpellCard(string name, float damage, Element element) : Card(name, damage, element)
{
    private readonly Dictionary<Element, Element> _elementEffectiveness = new()
    {
        { Element.Water, Element.Fire },
        { Element.Fire, Element.Normal },
        { Element.Normal, Element.Water }
    };

    public override float CalculateDamage(Card card)
    {
        if (card is MonsterCard monsterCard)
            return DamageModifiers.TryGetValue(monsterCard.Species, out float modifier) ? Damage * modifier : Damage;

        if (card.Element == Element)
            return Damage;

        if (Element == _elementEffectiveness[card.Element])
            return Damage * 2;

        return Damage * 0.5f;
    }
}