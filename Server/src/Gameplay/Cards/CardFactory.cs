using System.Xml;
using Shared.Card;
using Shared.Card.Types;
using Shared.DTOs;

namespace Server.Gameplay.Cards;

public static class CardFactory
{
    public const int PackSize = 5;

    public const int PackPrice = 20;

    private const float SpellCardProbability = .2f;

    private const float MinDamage = 1f;

    private const float MaxDamage = 111f;

    private static readonly float[] RarityProbabilities = [.5f, .3f, .15f, .04f, .01f];

    private static readonly Dictionary<Rarity, float> RarityMultiplier = new()
    {
        { Rarity.Common, 1f },
        { Rarity.Uncommon, 1.2f },
        { Rarity.Rare, 1.5f },
        { Rarity.Epic, 2f },
        { Rarity.Legendary, 3f }
    };

    private static Rarity GetRandomRarity(Random random)
    {
        float rdmValue = (float)random.NextDouble(); // Random value between 0 and 1
        float cumulativeProbability = 0f;

        for (int i = 0; i < RarityProbabilities.Length; i++)
        {
            cumulativeProbability += RarityProbabilities[i]; // Build cumulative probability
            //Console.WriteLine(cumulativeProbability);
            //Console.WriteLine(rdmValue);
            if (rdmValue <= cumulativeProbability)
            {
                return (Rarity)(i + 1); // +1 because Rarity enum starts at 1
            }
        }

        throw new InvalidOperationException($"Failed to generate a valid rarity. Random value: {rdmValue}");
    }


    public static Card[] GetCardPack(int packSize)
    {
        Card[] cardPack = new Card[packSize];
        Random random = new Random();

        for (int i = 0; i < packSize; i++)
        {
            Rarity rarity = GetRandomRarity(random);

            Card card = random.NextDouble() < SpellCardProbability
                ? GetSpellCard(
                    (float)(random.NextDouble() * (MaxDamage - MinDamage) + MinDamage) * RarityMultiplier[rarity],
                    (Element)random.Next(Enum.GetValues(typeof(Element)).Length) + 1,
                    rarity
                )
                : GetMonsterCard(
                    (float)(random.NextDouble() * (MaxDamage - MinDamage) + MinDamage) * RarityMultiplier[rarity],
                    (Element)random.Next(Enum.GetValues(typeof(Element)).Length) + 1,
                    (Species)random.Next(Enum.GetValues(typeof(Species)).Length) + 1,
                    rarity
                );

            cardPack[i] = card;
        }

        return cardPack;
    }

    public static Card GetCardFromCardDto(CardDto cardDto)
    {
        Card card = cardDto.CardType == CardType.Monster
            ? GetMonsterCard(cardDto.Damage, cardDto.Element, (Species)cardDto.Species!, cardDto.Rarity)
            : GetSpellCard(cardDto.Damage, cardDto.Element, cardDto.Rarity);
        
        card.Id = cardDto.CardId;
        
        return card;
    }

    public static SpellCard GetSpellCard(float damage, Element element, Rarity rarity)
    {
        SpellCard card = new SpellCard(element.ToString(), damage, element, rarity);

        if (element == Element.Water)
        {
            card.AddAttackModifier(Species.Knight, 999999f);
        }

        card.AddAttackModifier(Species.Kraken, 0f);

        return card;
    }

    public static MonsterCard GetMonsterCard(float damage, Element element, Species species, Rarity rarity)
    {
        MonsterCard card = new MonsterCard(species.ToString(), damage, element, species, rarity);

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
            case Species.FireElv:
            {
                card.AddAttackModifier(Species.Dragon, 0f);
                break;
            }
        }

        return card;
    }
}