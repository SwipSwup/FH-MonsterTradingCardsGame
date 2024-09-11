using System;
using MonsterTradingCardsGame.Networking.Client;
using MonsterTradingCardsGame.Core.Screen;

namespace MonsterTradingCardsGame.Gameplay
{
    public class MonsterTradingCardsGame
    {
        private Client _client;

        private IScreen _activeScreen;

        public MonsterTradingCardsGame()
        {
            InitializeConsole();
        }
        
        public MonsterTradingCardsGame(int width, int height)
        {
            InitializeConsole(width, height);
        }

        private void InitializeConsole(int width = -1, int height = -1)
        {
            Console.Title = "Monster Trading Cards Game";
            if (width < 0)
            {
                Console.WindowWidth = Console.LargestWindowWidth;
                Console.BufferWidth = Console.LargestWindowWidth;
            }

            if (height < 0)
            {
                Console.WindowHeight = Console.LargestWindowHeight;
                Console.BufferHeight = Console.LargestWindowHeight;
            }
            
            Console.CursorVisible = false;
            /*Console.WindowLeft = Console.BufferWidth / 2 - Console.WindowWidth / 2;
            Console.WindowTop = Console.BufferHeight / 2 - Console.WindowHeight / 2;*/
        }

        public static char GetUserInput()
        {
            return Console.ReadKey(true).KeyChar;
        }

        public static void WriteSpace(int space, ConsoleColor backgroundColor)
        {
            for (int i = 0; i < space; i++)
                Console.WriteLine();
        }

        public static void WriteLine(string message, ConsoleColor textColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}