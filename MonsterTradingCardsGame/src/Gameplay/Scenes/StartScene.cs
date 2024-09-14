using System;
<<<<<<< HEAD
using MonsterTradingCardsGame.Core.Input;
=======
using System.Xml;
>>>>>>> 856f5eb (Add login scene)
using MonsterTradingCardsGame.Core.Scene;

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
            switch (_menuSlot)
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
            _menuSlot++;

            if (_menuSlot > 2)
                _menuSlot = 2;
        }

        private void OnMenuLeft(ConsoleKeyInfo keyInfo)
        {
            _menuSlot--;

            if (_menuSlot < 0)
                _menuSlot = 0;
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
<<<<<<< HEAD
=======
        
        public override void Update()
        {
            ConsoleKeyInfo info = MonsterTradingCardsGame.GetKey();
>>>>>>> 856f5eb (Add login scene)

      


<<<<<<< HEAD
=======
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
                    break;
                }
                default:
                    return;
            }

            Draw();
        }

>>>>>>> 856f5eb (Add login scene)
        private void DrawMenu()
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