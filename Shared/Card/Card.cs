namespace Shared.Card;

public abstract class Card(string name, float damage, Element element, Rarity rarity)
{
    public int Id;
    
    public string Name { get; private set; } = name;

    public float Damage { get; private set; } = damage;

    public Element Element { get; private set; } = element;
    
    public Rarity Rarity { get; private set; } = rarity;

    protected Dictionary<Species, float> DamageModifiers = new();

    public void AddAttackModifier(Species species, float modifier)
    {
        DamageModifiers.Add(species, modifier);
    }

    public abstract float CalculateDamage(Card card);
}