using System;
using System.Net;
using MonsterTradingCardsGame.Core.Networking.Server;

namespace MonsterTradingCardsGame
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args[0] == "Server")
            {
                new Server().Start();
            }
            else
            {
                MonsterTradingCardsGame game = new MonsterTradingCardsGame(102, 50);
                game.Start();
            }
        }
    }
}