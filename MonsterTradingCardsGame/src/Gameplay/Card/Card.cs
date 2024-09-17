using System.Collections.Generic;

namespace MonsterTradingCardsGame.Gameplay.Card;

public abstract class Card
{
    public string Name { get; private set; }

    public float Damage { get; private set; }

    public Element Element { get; private set; }
    
    protected Dictionary<Specie, float> DamageModifiers = new();
    
    protected Card(string name, float damage, Element element)
    {
        Name = name;
        Damage = damage;
        Element = element;
    }

    public void AddAttackModifier(Specie specie, float modifier)
    {
        DamageModifiers.Add(specie, modifier);
    }

    public abstract float CalculateDamage(Card card);
}