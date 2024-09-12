namespace MonsterTradingCardsGame
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Gameplay.MonsterTradingCardsGame game = new Gameplay.MonsterTradingCardsGame(102, 50);

            game.Start();
        }
    }
}