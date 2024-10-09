using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MonsterTradingCardsGame.Core.Input;
using MonsterTradingCardsGame.Core.Networking.DTOs.Authentication;
using MonsterTradingCardsGame.Core.Scene;
using MonsterTradingCardsGame.Core.Settings;
using MonsterTradingCardsGame.Core.UI;
using MonsterTradingCardsGame.Gameplay.Client;

namespace MonsterTradingCardsGame.Gameplay.Scenes
{
    public class RegisterScene : Scene
    {
        private int _horizontalMenuIndex;
        private int _verticalMenuIndex;

        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _passwordConfirm = string.Empty;

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

        public override void Update()
        {
            Input.WaitForKeyPress();
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

        public override void Draw()
        {
            base.Draw();

            DrawTitle();

            DrawMenu();
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
                case 2:
                {
                    Input.BuildString(keyInfo, ref _passwordConfirm);
                    break;
                }
            }
        }

        private async void OnMenuSpace(ConsoleKeyInfo keyInfo)
        {
            if (_horizontalMenuIndex != 3)
                return;

            switch (_verticalMenuIndex)
            {
                case 0:
                    HttpResponseMessage response = await ServerClientManager.SendPostRequest(
                        "/register",
                        JsonSerializer.Serialize(
                            new UserCredentialsDto
                            {
                                Password = _password,
                                Username = _username
                            })
                    );

                    Console.WriteLine(response.StatusCode);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        ServerClientManager.Token = JsonSerializer.Deserialize<AuthenticationResponseDto>(await response.Content.ReadAsStringAsync()).Token;
                        
                        Game.LoadScene(new DashboardScene());
                    }

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
                _horizontalMenuIndex = 3;
        }

        private void OnMenuDown(ConsoleKeyInfo keyInfo)
        {
            _horizontalMenuIndex++;

            if (_horizontalMenuIndex > 3)
                _horizontalMenuIndex = 0;
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
            Gui.WriteLine("Confirm password:");
            Gui.TextField(
                string.Concat(Enumerable.Repeat("*", _passwordConfirm.Length)),
                _horizontalMenuIndex == 2,
                GameSettings.MAX_PASSWORD_LENGTH,
                new GuiStyle { Offset = 8 }
            );

            Gui.SpaceVertical(1);
            Gui.SpaceHorizontal(8);
            Gui.Button("Register", _horizontalMenuIndex == 3 && _verticalMenuIndex == 0);

            Gui.SpaceHorizontal(1);
            Gui.Button("Back", _horizontalMenuIndex == 3 && _verticalMenuIndex == 1);
        }
    }
}