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
            card.AddAttackModifier(Species.Knight, 999999f);
        }

        card.AddAttackModifier(Species.Kraken, 0f);

        return card;
    }

    public static MonsterCard GetMonsterCard(string name, float damage, Element element, Species species)
    {
        MonsterCard card = new MonsterCard(name, damage, element, species);

        switch (species)
        {
            case Species.Goblin:
            {
                card.AddAttackModifier(Species.Dragon, 0f);
                break;
            }
            case Species.Orc:
            {
                card.AddAttackModifier(Species.Wizard, 0f);
                break;
            }
            case Species.FireElf:
            {
                card.AddAttackModifier(Species.Dragon, 0f);
                break;
            }
        }
        
        return card;
    }
}