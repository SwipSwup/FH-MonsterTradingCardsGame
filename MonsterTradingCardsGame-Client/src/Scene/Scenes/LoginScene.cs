using MonsterTradingCardsGame.Core.Input;
using MonsterTradingCardsGame.Core.Settings;
using MonsterTradingCardsGame.Core.UI;

namespace MonsterTradingCardsGame_Client.Scene.Scenes
{
    public class LoginScene : IScene
    {
        private int _horizontalMenuIndex;
        private int _verticalMenuIndex;

        private string _username = string.Empty;
        private string _password = string.Empty;

        public void Update()
        {
            Input.WaitForKeyPress();
        }

        public void Initialize()
        {
            Input.RegisterKeyAction(ConsoleKey.LeftArrow, OnMenuLeft);
            Input.RegisterKeyAction(ConsoleKey.RightArrow, OnMenuRight);
            Input.RegisterKeyAction(ConsoleKey.UpArrow, OnMenuUp);
            Input.RegisterKeyAction(ConsoleKey.DownArrow, OnMenuDown);
            Input.RegisterKeyAction(ConsoleKey.Spacebar, OnMenuSpace);

            Input.RegisterDefaultAction(OnKeyPressed);
        }
        
        public void Destroy()
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
                    SceneManager.LoadScene(new StartScene());
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

            if (_horizontalMenuIndex > 2)
                _horizontalMenuIndex = 0;
        }


        public void Draw()
        {
            DrawTitle();

            DrawMenu();
        }

        private void DrawTitle()
        {
            Gui.WriteLine(
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

        private void DrawMenu()
        {
            Gui.SpaceHorizontal(8);
            Gui.WriteLine("Username:");
            Gui.TextField(
                _username,
                _horizontalMenuIndex == 0,
                GameSettings.MAX_USERNAME_LENGTH,
                new GuiStyle { Offset = 8 }
            );

            Gui.SpaceVertical(1);
            Gui.SpaceHorizontal(8);
            Gui.WriteLine("Password:");
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
    }
}