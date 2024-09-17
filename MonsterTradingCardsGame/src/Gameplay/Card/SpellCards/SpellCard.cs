using System.Collections.Generic;
using MonsterTradingCardsGame.Gameplay.Card.MonsterCards;

namespace MonsterTradingCardsGame.Gameplay.Card.SpellCards;

public class SpellCard : Card
{
    private readonly Dictionary<Element, Element> _elementEffectiveness = new()
    {
        { Element.Water, Element.Fire },
        { Element.Fire, Element.Normal },
        { Element.Normal, Element.Water }
    };
    
    public SpellCard(string name, float damage, Element element) : base(name, damage, element)
    {
    }

    public override float CalculateDamage(Card card)
    {
        if (card is MonsterCard monsterCard)
        {
            return DamageModifiers.TryGetValue(monsterCard.Specie, out var modifier) ? Damage * modifier : Damage;
        }

        if (card.Element == Element)
            return Damage;
        
        if(Element == _elementEffectiveness[card.Element])
            return Damage * 2;
        
        return Damage * 0.5f;
    }
}