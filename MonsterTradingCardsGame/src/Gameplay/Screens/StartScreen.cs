using MonsterTradingCardsGame.Core.Screen;

namespace MonsterTradingCardsGame.Gameplay.Screens
{
    public class StartScreen : IScreen
    {
        
        
        public void Print()
        {
            MonsterTradingCardsGame.WriteLine(Art.Banner);
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}