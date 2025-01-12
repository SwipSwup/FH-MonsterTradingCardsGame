using Server.Gameplay.Cards;
using Shared.Card;
using Shared.DTOs;

namespace Server.Gameplay.Battle;

public struct BattleEntity
{
    public int UserId;

    public List<Card> Deck;

    public void LoadDeck(IEnumerable<CardDto> deck)
    {
        Deck = deck.Select(CardFactory.GetCardFromCardDto).ToList();
    }
}