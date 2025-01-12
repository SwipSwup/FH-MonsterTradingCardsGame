using Shared.Card;
using Shared.DTOs;

namespace Server.Gameplay.Battle;

public static class BattleManager
{
    public const int MmrRange = 300;
    
    private static readonly int UpperBattleRoundBound = 100;
    
    public static BattleLog Battle(BattleEntity player1, BattleEntity player2) 
    {
        Random random = new Random();
        
        BattleLog battleLog = new BattleLog();
        
        for (int i = 0; i < UpperBattleRoundBound; i++)
        {
            Card cardP1 = player1.Deck[random.Next(player1.Deck.Count)];
            Card cardP2 = player2.Deck[random.Next(player2.Deck.Count)];

            float damageDifference = cardP1.CalculateDamage(cardP2) - cardP2.CalculateDamage(cardP1);
            
            BattleRoundDto round = new BattleRoundDto
            {
                RoundNumber = i + 1,
                Card1Id = cardP1.Id,
                Card2Id = cardP2.Id,
            };

            switch (damageDifference)
            {
                case < 0:
                    round.WinnerCardId = cardP2.Id;
                    round.Result = "card_2_wins";
                    player1.Deck.Remove(cardP1);
                    player2.Deck.Add(cardP1);
                    break;
                case > 0:
                    round.WinnerCardId = cardP1.Id;
                    round.Result = "card_1_wins";
                    player2.Deck.Remove(cardP2);
                    player1.Deck.Add(cardP2);
                    break;
                default:
                    round.Result = "draw";
                    continue;
            }
            
            battleLog.Rounds.Add(round);

            if (player1.Deck.Count == 0)
            {
                battleLog.WinnerUserId = player2.UserId;
                battleLog.LoserUserId = player1.UserId;
                
               return battleLog; 
            }
            
            if (player2.Deck.Count == 0)
            {
                battleLog.WinnerUserId = player1.UserId;
                battleLog.LoserUserId = player2.UserId;
                
                return battleLog; 
            }
        }
        
        return battleLog;
    }
    
}