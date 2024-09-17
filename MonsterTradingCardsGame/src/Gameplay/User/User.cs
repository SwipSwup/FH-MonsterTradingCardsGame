using System.Collections.Generic;

namespace MonsterTradingCardsGame.Gameplay.User;

public class User
{
    public List<StackCardWrapper> Stack { get; private set; } = new();

    public List<int> Deck { get; private set; } = new();
    
    public void AddCard(Card.Card card)
    {
        Stack.Add(new StackCardWrapper(card));
    }

    public bool TryAddCardToDeck(int cardIndex)
    {
        Stack[cardIndex].TrySetInDeck();
        
        return false;
    } 

    public void RemoveCardFromDeck(int index)
    {
        
    }
}

public struct StackCardWrapper /*: IEquatable<UserCardWrapper>*/
{
    public Card.Card Card { get; private set; }

    public bool MarkedForTrade { get; private set; }
    public bool InDeck  { get; private set; }


    public StackCardWrapper(Card.Card card)
    {
        Card = card;
        InDeck = false;
    }

    public bool TrySetInDeck()
    {
        if(MarkedForTrade) return false;
        
        return !InDeck;
    }
}