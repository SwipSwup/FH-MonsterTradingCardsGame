using System;
using MonsterTradingCardsGame.Core.Scene;
using MonsterTradingCardsGame.Gameplay.Scenes;

namespace MonsterTradingCardsGame
{
    public class MonsterTradingCardsGame
    {
        private Scene _activeScene;

        public MonsterTradingCardsGame()
        {
            InitializeConsole();
        }

        public MonsterTradingCardsGame(int width, int height)
        {
            InitializeConsole(width, height);
            LoadScene(new StartScene());
        }

        public void Start()
        {
            char input;

            while (true)
            {
                _activeScene.Update();
                _activeScene.Draw();
            }
        }

        public void Stop()
        {
            Environment.Exit(0);
        }

        public void LoadScene(Scene scene)
        {
            _activeScene?.Destroy();
            Console.Clear();
            
            _activeScene = scene;
            _activeScene.Initialize(this);
            _activeScene.Draw();
        }

        private void InitializeConsole(int width = -1, int height = -1)
        {
            Console.Title = "Titan Clash";
            if (width < 0)
            {
                Console.WindowWidth = Console.LargestWindowWidth;
                Console.BufferWidth = Console.LargestWindowWidth;
            }
            else
            {
                Console.WindowWidth = width;
                Console.BufferWidth = width;
            }

            if (height < 0)
            {
                Console.WindowHeight = Console.LargestWindowHeight;
                Console.BufferHeight = Console.LargestWindowHeight;
            }
            else
            {
                Console.WindowHeight = height;
                Console.BufferHeight = height;
            }

            Console.CursorVisible = false;
            /*Console.WindowLeft = Console.BufferWidth / 2 - Console.WindowWidth / 2;
            Console.WindowTop = Console.BufferHeight / 2 - Console.WindowHeight / 2;*/
        }

        public static ConsoleKeyInfo GetKey()
        {
            return Console.ReadKey(true);
        }

        public static void WriteLine(string message, ConsoleColor textColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            WriteInternal(message, textColor, backgroundColor, Console.WriteLine);
        }

        public static void Write(string message, ConsoleColor textColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            WriteInternal(message, textColor, backgroundColor, Console.Write);
        }

        private static void WriteInternal(string message, ConsoleColor textColor, ConsoleColor backgroundColor,
            Action<string> write)
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
            write(message);
            Console.ResetColor();
        }
    }
}