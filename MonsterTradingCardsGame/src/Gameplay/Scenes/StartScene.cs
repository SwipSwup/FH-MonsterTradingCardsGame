using System;
using MonsterTradingCardsGame.Core.Input;
using System.Xml;
using MonsterTradingCardsGame.Core.Scene;
using MonsterTradingCardsGame.Core.UI;

namespace MonsterTradingCardsGame.Gameplay.Scenes
{
    public class StartScene : Scene
    {
        private int _menuIndex;
        
        public override void Draw()
        {
            base.Draw();

            DrawTitle();

            DrawMenu();
        }

        public override void Initialize(MonsterTradingCardsGame game)
        {
            base.Initialize(game);

            Input.RegisterKeyAction(ConsoleKey.LeftArrow, OnMenuLeft);
            Input.RegisterKeyAction(ConsoleKey.RightArrow, OnMenuRight);
            Input.RegisterKeyAction(ConsoleKey.Spacebar, OnMenuSpace);
        }
        
        public override void Destroy()
        {
            Input.UnregisterKeyAction(ConsoleKey.LeftArrow, OnMenuLeft);
            Input.UnregisterKeyAction(ConsoleKey.RightArrow, OnMenuRight);
            Input.UnregisterKeyAction(ConsoleKey.Spacebar, OnMenuSpace);
        }
        
        public override void Update()
        {
            Input.WaitForKeyPress();
        }

        private void OnMenuSpace(ConsoleKeyInfo obj)
        {
            switch (_menuIndex)
            {
                case 0:
                    Game.LoadScene(new LoginScene());
                    return;
                case 1:
                    Game.LoadScene(new RegisterScene());
                    return;
                case 2:
                    Game.Stop();
                    return;
            }
        }

        private void OnMenuRight(ConsoleKeyInfo keyInfo)
        {
            _menuIndex++;

            if (_menuIndex > 2)
                _menuIndex = 2;
        }

        private void OnMenuLeft(ConsoleKeyInfo keyInfo)
        {
            _menuIndex--;

            if (_menuIndex < 0)
                _menuIndex = 0;
        }

        private void DrawTitle()
        {
            Console.WriteLine(
                "\n  \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2588   \u2588\u2588\u2588\u2588\u2588                              \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2588\u2588                    \u2588\u2588\u2588\u2588\u2588     \n" +
                " \u2591\u2588\u2591\u2591\u2591\u2588\u2588\u2588\u2591\u2591\u2591\u2588 \u2591\u2591\u2591   \u2591\u2591\u2588\u2588\u2588                              \u2588\u2588\u2588\u2591\u2591\u2591\u2591\u2591\u2588\u2588\u2588\u2591\u2591\u2588\u2588\u2588                   \u2591\u2591\u2588\u2588\u2588      \n" +
                " \u2591   \u2591\u2588\u2588\u2588  \u2591  \u2588\u2588\u2588\u2588  \u2588\u2588\u2588\u2588\u2588\u2588\u2588    \u2588\u2588\u2588\u2588\u2588\u2588   \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588      \u2588\u2588\u2588     \u2591\u2591\u2591  \u2591\u2588\u2588\u2588   \u2588\u2588\u2588\u2588\u2588\u2588    \u2588\u2588\u2588\u2588\u2588  \u2591\u2588\u2588\u2588\u2588\u2588\u2588\u2588  \n" +
                "     \u2591\u2588\u2588\u2588    \u2591\u2591\u2588\u2588\u2588 \u2591\u2591\u2591\u2588\u2588\u2588\u2591    \u2591\u2591\u2591\u2591\u2591\u2588\u2588\u2588 \u2591\u2591\u2588\u2588\u2588\u2591\u2591\u2588\u2588\u2588    \u2591\u2588\u2588\u2588          \u2591\u2588\u2588\u2588  \u2591\u2591\u2591\u2591\u2591\u2588\u2588\u2588  \u2588\u2588\u2588\u2591\u2591   \u2591\u2588\u2588\u2588\u2591\u2591\u2588\u2588\u2588 \n" +
                "     \u2591\u2588\u2588\u2588     \u2591\u2588\u2588\u2588   \u2591\u2588\u2588\u2588      \u2588\u2588\u2588\u2588\u2588\u2588\u2588  \u2591\u2588\u2588\u2588 \u2591\u2588\u2588\u2588    \u2591\u2588\u2588\u2588          \u2591\u2588\u2588\u2588   \u2588\u2588\u2588\u2588\u2588\u2588\u2588 \u2591\u2591\u2588\u2588\u2588\u2588\u2588  \u2591\u2588\u2588\u2588 \u2591\u2588\u2588\u2588 \n" +
                "     \u2591\u2588\u2588\u2588     \u2591\u2588\u2588\u2588   \u2591\u2588\u2588\u2588 \u2588\u2588\u2588 \u2588\u2588\u2588\u2591\u2591\u2588\u2588\u2588  \u2591\u2588\u2588\u2588 \u2591\u2588\u2588\u2588    \u2591\u2591\u2588\u2588\u2588     \u2588\u2588\u2588 \u2591\u2588\u2588\u2588  \u2588\u2588\u2588\u2591\u2591\u2588\u2588\u2588  \u2591\u2591\u2591\u2591\u2588\u2588\u2588 \u2591\u2588\u2588\u2588 \u2591\u2588\u2588\u2588 \n" +
                "     \u2588\u2588\u2588\u2588\u2588    \u2588\u2588\u2588\u2588\u2588  \u2591\u2591\u2588\u2588\u2588\u2588\u2588 \u2591\u2591\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588 \u2588\u2588\u2588\u2588 \u2588\u2588\u2588\u2588\u2588    \u2591\u2591\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2588\u2588\u2588\u2591\u2591\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588 \u2588\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2588\u2588 \u2588\u2588\u2588\u2588\u2588\n" +
                "    \u2591\u2591\u2591\u2591\u2591    \u2591\u2591\u2591\u2591\u2591    \u2591\u2591\u2591\u2591\u2591   \u2591\u2591\u2591\u2591\u2591\u2591\u2591\u2591 \u2591\u2591\u2591\u2591 \u2591\u2591\u2591\u2591\u2591      \u2591\u2591\u2591\u2591\u2591\u2591\u2591\u2591\u2591  \u2591\u2591\u2591\u2591\u2591  \u2591\u2591\u2591\u2591\u2591\u2591\u2591\u2591 \u2591\u2591\u2591\u2591\u2591\u2591  \u2591\u2591\u2591\u2591 \u2591\u2591\u2591\u2591\u2591 \n\n\n\n"
            );
        }

        private int _menuSlot;
        
        private void DrawMenu()
        {
            MonsterTradingCardsGame.WriteLine(
                "                                                 /   \\\n" +
                "                       _                 )      ((   ))     (\n" +
                "                      (@)               /|\\      ))_((     /|\\                 _\n" +
                "                      |-|`\\            / | \\    (/\\|/\\)   / | \\               (@)\n" +
                "                      | | ------------/--|-voV---\\`|'/--Vov-|--\\--------------|-|\n" +
                "                      |-|                  '^`   (o o)  '^`                   | |\n" +
                "                      | |                        `\\Y/'                        |-|\n" +
                "                      |-|                                                     | |"
            );

            MonsterTradingCardsGame.Write("                      | |        ");
            Gui.Button("Login", _menuIndex == 0);

            Gui.SpaceHorizontal(7);
            Gui.Button("Register", _menuIndex == 1);

            Gui.SpaceHorizontal(7);
            Gui.Button("Exit", _menuIndex == 2);
            MonsterTradingCardsGame.Write("        |-|\n");

            MonsterTradingCardsGame.WriteLine(
                "                      |_|_____________________________________________________| |\n" +
                "                      (@)       l   /\\ /         ( (       \\ /\\   l         `\\|-|\n" +
                "                                l /   V           \\ \\       V   \\ l           (@)\n" +
                "                                l/                _) )_          \\I\n" +
                "                                                  `\\ /'\n" +
                "                                                    `"
            );
        }
    }
}