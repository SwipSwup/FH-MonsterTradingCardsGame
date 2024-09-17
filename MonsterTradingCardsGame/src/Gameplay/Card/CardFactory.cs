using MonsterTradingCardsGame.Gameplay.Card.MonsterCards;
using MonsterTradingCardsGame.Gameplay.Card.SpellCards;

namespace MonsterTradingCardsGame.Gameplay.Card;

public static class CardFactory
{
    public static SpellCard GetSpellCard(string name, float damage, Element element)
    {
        SpellCard card = new SpellCard(name, damage, element);

        if (element == Element.Water)
        {
            card.AddAttackModifier(Specie.Knights, float.MaxValue);
        }

        card.AddAttackModifier(Specie.Kraken, 0f);

        return card;
    }

    public static MonsterCard GetMonsterCard(string name, float damage, Element element, Specie specie)
    {
        MonsterCard card = new MonsterCard(name, damage, element, specie);

        switch (specie)
        {
            case Specie.Goblin:
            {
                card.AddAttackModifier(Specie.Dragon, 0f);
                break;
            }
            case Specie.Ork:
            {
                card.AddAttackModifier(Specie.Wizard, 0f);
                break;
            }
            case Specie.FireElves:
            {
                card.AddAttackModifier(Specie.Dragon, 0f);
                break;
            }
        }
        
        return card;
    }
}