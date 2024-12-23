using MonsterTradingCardsGame_Client.Scene;
using MonsterTradingCardsGame_Client.Scene.Scenes;

namespace MonsterTradingCardsGame_Client
{
    public class MonsterTradingCardsGame
    {
        public static MonsterTradingCardsGame? Instance;
        
        //TODO SceneManager
        private IScene _activeScene;

        public MonsterTradingCardsGame(int width, int height)
        {
            Instance ??= this;

            InitializeConsole(width, height);
            //LoadScene(new StartScene());
            LoadScene(new StartScene());
        }

        public void Start()
        {
            while (true)
            {
                SceneManager.UpdateActiveScene();
            }
        }

        public static void Stop()
        {
            Environment.Exit(0);
        }

        public void LoadScene(IScene scene)
        {
            
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
    }
}