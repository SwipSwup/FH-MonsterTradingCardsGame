using System;
using System.Collections.Generic;

namespace MonsterTradingCardsGame.Gameplay.User;

public class User
{
    public List<StackCardWrapper> Stack { get; private set; } = new();

    public List<int> Deck { get; private set; } = new();

   
}

public struct StackCardWrapper /*: IEquatable<UserCardWrapper>*/
{
   
}