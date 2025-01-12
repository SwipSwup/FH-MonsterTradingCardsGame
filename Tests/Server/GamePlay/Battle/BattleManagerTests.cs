using Server.Gameplay.Battle;
using Shared.Card;
using Shared.DTOs;

namespace Tests.Server.GamePlay.Battle
{
    [TestFixture]
    public class BattleManagerTests
    {
        [Test]
        public void Battle_ShouldReturnCorrectWinner_WhenPlayer1Wins()
        {
            BattleEntity player1 = CreateBattleEntity(1, [
                new CardDto { CardId = 1, Damage = 50, Element = Element.Fire },
                new CardDto { CardId = 2, Damage = 60, Element = Element.Fire }
            ]);

            BattleEntity player2 = CreateBattleEntity(2, [
                new CardDto { CardId = 3, Damage = 40, Element = Element.Fire },
                new CardDto { CardId = 4, Damage = 30, Element = Element.Fire }
            ]);

            BattleLog battleLog = BattleManager.Battle(player1, player2);

            Assert.That(battleLog.WinnerUserId, Is.EqualTo(player1.UserId));
            Assert.That(battleLog.LoserUserId, Is.EqualTo(player2.UserId));
        }
        
        [Test]
        public void Battle_ShouldHandleDrawScenario()
        {
            BattleEntity player1 = CreateBattleEntity(1, [new CardDto { CardId = 1, Damage = 50, Element = Element.Fire }]);
            BattleEntity player2 = CreateBattleEntity(2, [new CardDto { CardId = 2, Damage = 50, Element = Element.Fire }]);

            BattleLog battleLog = BattleManager.Battle(player1, player2);

            Assert.That(battleLog.WinnerUserId, Is.EqualTo(null)); 
            Assert.That(battleLog.LoserUserId, Is.EqualTo(null)); 
        }
        

        [Test]
        public void Battle_ShouldStopEarly_WhenOnePlayerRunsOutOfCards()
        {
            BattleEntity player1 = CreateBattleEntity(1, [
                new CardDto { CardId = 1, Damage = 50, Element = Element.Fire },
                new CardDto { CardId = 2, Damage = 30, Element = Element.Water }
            ]);

            BattleEntity player2 = CreateBattleEntity(2, [
                new CardDto { CardId = 3, Damage = 70, Element = Element.Normal },
                new CardDto { CardId = 4, Damage = 60, Element = Element.Water }
            ]);

            BattleLog battleLog = BattleManager.Battle(player1, player2);

            Assert.That(battleLog.Rounds.Count, Is.EqualTo(2));
        }

        private BattleEntity CreateBattleEntity(int userId, List<CardDto> deck)
        {
            BattleEntity entity = new BattleEntity
            {
                UserId = userId
            };
            entity.LoadDeck(deck);
            return entity;
        }
    }
}