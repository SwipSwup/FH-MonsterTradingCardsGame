using System.Net;
using MonsterTradingCardsGame.Core.Networking.Server;

namespace MonsterTradingCardsGame
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //MonsterTradingCardsGame game = new MonsterTradingCardsGame(102, 50);
            //game.Start();
            new Server().Start();
        }
    }
}