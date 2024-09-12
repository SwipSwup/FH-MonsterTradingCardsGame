using System;
using System.Xml;
using MonsterTradingCardsGame.Core.Screen;

namespace MonsterTradingCardsGame.Gameplay.Scenes
{
    public class StartScene : Scene
    {
        public override void Draw()
        {
            base.Draw();

            DrawTitle();

            DrawMenu();
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

        public override void Update()
        {
            ConsoleKeyInfo info = MonsterTradingCardsGame.GetKey();

            switch (info.Key)
            {
                case ConsoleKey.A:
                {
                    _menuSlot--;

                    if (_menuSlot < 0)
                        _menuSlot = 2;

                    break;
                }
                case ConsoleKey.D:
                {
                    _menuSlot++;

                    if (_menuSlot > 2)
                        _menuSlot = 0;

                    break;
                }
                case ConsoleKey.Spacebar:
                {
                    switch (_menuSlot)
                    {
                        case 2:
                            Environment.Exit(0);
                            break;
                    }
                    break;
                }
                default:
                    return;
            }

            Draw();
        }

        private int _menuSlot = 0;

        public void DrawMenu()
        {
            Console.WriteLine(
                "                                                 /   \\\n" +
                "                       _                 )      ((   ))     (\n" +
                "                      (@)               /|\\      ))_((     /|\\                 _\n" +
                "                      |-|`\\            / | \\    (/\\|/\\)   / | \\               (@)\n" +
                "                      | | ------------/--|-voV---\\`|'/--Vov-|--\\--------------|-|\n" +
                "                      |-|                  '^`   (o o)  '^`                   | |\n" +
                "                      | |                        `\\Y/'                        |-|\n" +
                "                      |-|                                                     | |"
            );

            Console.Write("                      | |        ");
            MonsterTradingCardsGame.Write(
                "[Login]",
                _menuSlot == 0 ? ConsoleColor.Black : ConsoleColor.White,
                _menuSlot == 0 ? ConsoleColor.White : ConsoleColor.Black
            );
            
            Console.Write("       ");
            MonsterTradingCardsGame.Write(
                "[Register]",
                _menuSlot == 1 ? ConsoleColor.Black : ConsoleColor.White,
                _menuSlot == 1 ? ConsoleColor.White : ConsoleColor.Black
            );

            Console.Write("       ");
            MonsterTradingCardsGame.Write(
                "[Exit]",
                _menuSlot == 2 ? ConsoleColor.Black : ConsoleColor.White,
                _menuSlot == 2 ? ConsoleColor.White : ConsoleColor.Black
            );
            Console.Write("        |-|\n");

            Console.WriteLine(
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