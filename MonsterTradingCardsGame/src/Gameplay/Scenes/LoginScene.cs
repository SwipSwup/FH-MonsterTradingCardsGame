using System;
<<<<<<< HEAD
using System.Linq;
using MonsterTradingCardsGame.Core.Input;
using MonsterTradingCardsGame.Core.Scene;
using MonsterTradingCardsGame.Core.Settings;
<<<<<<< Updated upstream
=======
using MonsterTradingCardsGame.Core.Scene;
>>>>>>> 856f5eb (Add login scene)
=======
using MonsterTradingCardsGame.Core.UI;
>>>>>>> Stashed changes

namespace MonsterTradingCardsGame.Gameplay.Scenes
{
    public class LoginScene : Scene
    {
<<<<<<< Updated upstream
<<<<<<< HEAD
        private int _menuSlotHorizontal;
        private int _menuSlotVertical;
        
        private string _username = String.Empty;
        private string _password = String.Empty ;
        
=======
        private int _horizontalMenuIndex;
        private int _verticalMenuIndex;

        private string _username = string.Empty;
        private string _password = string.Empty;

>>>>>>> Stashed changes
        public override void Update()
        {
            Input.WaitForKeyPress();
        }

        public override void Initialize(MonsterTradingCardsGame game)
        {
            base.Initialize(game);

            Input.RegisterKeyAction(ConsoleKey.LeftArrow, OnMenuLeft);
            Input.RegisterKeyAction(ConsoleKey.RightArrow, OnMenuRight);
            Input.RegisterKeyAction(ConsoleKey.UpArrow, OnMenuUp);
            Input.RegisterKeyAction(ConsoleKey.DownArrow, OnMenuDown);
            Input.RegisterKeyAction(ConsoleKey.Spacebar, OnMenuSpace);

            Input.RegisterDefaultAction(OnKeyPressed);
        }
        
        public override void Destroy()
        {
            Input.RegisterKeyAction(ConsoleKey.LeftArrow, OnMenuLeft);
            Input.RegisterKeyAction(ConsoleKey.RightArrow, OnMenuRight);
            Input.RegisterKeyAction(ConsoleKey.UpArrow, OnMenuUp);
            Input.RegisterKeyAction(ConsoleKey.DownArrow, OnMenuDown);
            Input.UnregisterKeyAction(ConsoleKey.Spacebar, OnMenuSpace);

            Input.UnregisterDefaultAction(OnKeyPressed);
        }

        private void OnKeyPressed(ConsoleKeyInfo keyInfo)
        {
            switch (_horizontalMenuIndex)
            {
                case 0:
                {
                    Input.BuildString(keyInfo, ref _username);
                    break;
                }
                case 1:
                {
                    Input.BuildString(keyInfo, ref _password);
                    break;
                }
            }
        }

        private void OnMenuSpace(ConsoleKeyInfo keyInfo)
        {
            if (_horizontalMenuIndex != 2)
                return;

            switch (_verticalMenuIndex)
            {
                case 0:
                    return;
                case 1:
                    Game.LoadScene(new StartScene());
                    return;
            }
        }

        private void OnMenuLeft(ConsoleKeyInfo keyInfo)
        {
            _verticalMenuIndex--;

            if (_verticalMenuIndex < 0)
                _verticalMenuIndex = 0;
        }

        private void OnMenuRight(ConsoleKeyInfo keyInfo)
        {
            _verticalMenuIndex++;

            if (_verticalMenuIndex > 1)
                _verticalMenuIndex = 1;
        }

        private void OnMenuUp(ConsoleKeyInfo keyInfo)
        {
            _horizontalMenuIndex--;

            if (_horizontalMenuIndex < 0)
                _horizontalMenuIndex = 2;
        }

        private void OnMenuDown(ConsoleKeyInfo keyInfo)
        {
            _horizontalMenuIndex++;

<<<<<<< Updated upstream
            if (_menuSlotHorizontal > 2)
                _menuSlotHorizontal = 0;
        }


        public override void Destroy()
        {
            Input.UnregisterKeyAction(ConsoleKey.A, OnMenuLeft);
            Input.UnregisterKeyAction(ConsoleKey.D, OnMenuRight);
            Input.UnregisterKeyAction(ConsoleKey.W, OnMenuUp);
            Input.UnregisterKeyAction(ConsoleKey.S, OnMenuDown);
            Input.UnregisterKeyAction(ConsoleKey.Spacebar, OnMenuSpace);
            
            Input.UnregisterDefaultAction(OnKeyPressed);
=======
        public override void Update()
        {
>>>>>>> 856f5eb (Add login scene)
=======
            if (_horizontalMenuIndex > 2)
                _horizontalMenuIndex = 0;
>>>>>>> Stashed changes
        }

        public override void Draw()
        {
            base.Draw();
<<<<<<< HEAD

            DrawTitle();

            DrawMenu();
=======
            
            DrawTitle();
>>>>>>> 856f5eb (Add login scene)
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
<<<<<<< HEAD

        private void DrawMenu()
        {
            Gui.SpaceHorizontal(8);
            MonsterTradingCardsGame.WriteLine("Username:");
            Gui.TextField(
                _username,
                _horizontalMenuIndex == 0,
                GameSettings.MAX_USERNAME_LENGTH,
                new GuiStyle { Offset = 8 }
            );

            Gui.SpaceVertical(1);
            Gui.SpaceHorizontal(8);
            MonsterTradingCardsGame.WriteLine("Password:");
            Gui.TextField(
                string.Concat(Enumerable.Repeat("*", _password.Length)),
                _horizontalMenuIndex == 1,
                GameSettings.MAX_PASSWORD_LENGTH,
                new GuiStyle { Offset = 8 }
            );

            Gui.SpaceVertical(1);
            Gui.SpaceHorizontal(8);
            Gui.Button("Login", _horizontalMenuIndex == 2 && _verticalMenuIndex == 0);

            Gui.SpaceHorizontal(1);
            Gui.Button("Back", _horizontalMenuIndex == 2 && _verticalMenuIndex == 1);
        }
=======
>>>>>>> 856f5eb (Add login scene)
    }
}