using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MonsterTradingCardsGame.Gameplay.Card;

public abstract class Card 
{
    public string Name { get; private set; }

    public float Damage { get; private set; }

    public Element Element { get; private set; }
    
    protected Dictionary<Species, float> DamageModifiers = new();
    
    protected Card(string name, float damage, Element element)
    {
        Name = name;
        Damage = damage;
        Element = element;
    }

    public void AddAttackModifier(Species species, float modifier)
    {
        DamageModifiers.Add(species, modifier);
    }

    public abstract float CalculateDamage(Card card);
}