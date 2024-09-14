using System;
<<<<<<< HEAD
using System.Linq;
using MonsterTradingCardsGame.Core.Input;
using MonsterTradingCardsGame.Core.Scene;
using MonsterTradingCardsGame.Core.Settings;
=======
using MonsterTradingCardsGame.Core.Scene;
>>>>>>> 856f5eb (Add login scene)

namespace MonsterTradingCardsGame.Gameplay.Scenes
{
    public class LoginScene : Scene
    {
<<<<<<< HEAD
        private int _menuSlotHorizontal;
        private int _menuSlotVertical;
        
        private string _username = String.Empty;
        private string _password = String.Empty ;
        
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

        private void OnKeyPressed(ConsoleKeyInfo keyInfo)
        {
            switch (_menuSlotHorizontal)
            {
                case 0:
                {
                    ProcessStringBuilding(keyInfo, ref _username);
                    break;
                }
                case 1:
                {
                    ProcessStringBuilding(keyInfo, ref _password);
                    break;
                }
                
            }
        }

        private void ProcessStringBuilding(ConsoleKeyInfo keyInfo, ref string target)
        {
            if (keyInfo.Key == ConsoleKey.Backspace)
            {
                target = target.Substring(0, target.Length - 1);
                return;
            }

                    
            if(keyInfo.Key < ConsoleKey.A || keyInfo.Key > ConsoleKey.Z)
                return;
                    
            target += keyInfo.KeyChar;
        }


        private void OnMenuSpace(ConsoleKeyInfo keyInfo)
        {
            if(_menuSlotHorizontal != 2)
                return;
                    
            switch (_menuSlotVertical)
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
            _menuSlotVertical--;

            if (_menuSlotVertical < 0)
                _menuSlotVertical = 0;
        }
        
        private void OnMenuRight(ConsoleKeyInfo keyInfo)
        {
            _menuSlotVertical++;

            if (_menuSlotVertical > 1)
                _menuSlotVertical = 1;
        }
        
        private void OnMenuUp(ConsoleKeyInfo keyInfo)
        {
            _menuSlotHorizontal--;

            if (_menuSlotHorizontal < 0)
                _menuSlotHorizontal = 2;
        }
        
        private void OnMenuDown(ConsoleKeyInfo keyInfo)
        {
            _menuSlotHorizontal++;

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
            MonsterTradingCardsGame.WriteLine("\tUsername:");
            MonsterTradingCardsGame.Write("\t");
            MonsterTradingCardsGame.WriteLine(
                $"[{_username}{String.Concat(Enumerable.Repeat("_", GameSettings.MAX_USERNAME_LENGTH - _username.Length))}]",
                _menuSlotHorizontal == 0 ? ConsoleColor.Black : ConsoleColor.White,
                _menuSlotHorizontal == 0 ? ConsoleColor.White : ConsoleColor.Black
            );

            MonsterTradingCardsGame.WriteLine("\n\tPassword:");
            MonsterTradingCardsGame.Write("\t");
            MonsterTradingCardsGame.WriteLine(
                $"[{String.Concat(Enumerable.Repeat("*", _password.Length))}{String.Concat(Enumerable.Repeat("_", GameSettings.MAX_PASSWORD_LENGTH - _password.Length))}]",
                _menuSlotHorizontal == 1 ? ConsoleColor.Black : ConsoleColor.White,
                _menuSlotHorizontal == 1 ? ConsoleColor.White : ConsoleColor.Black
            );


            MonsterTradingCardsGame.Write("\n\t");
            MonsterTradingCardsGame.Write(
                "[Login]",
                _menuSlotHorizontal == 2 && _menuSlotVertical == 0 ? ConsoleColor.Black : ConsoleColor.White,
                _menuSlotHorizontal == 2 && _menuSlotVertical == 0 ? ConsoleColor.White : ConsoleColor.Black
            );

            MonsterTradingCardsGame.Write(" ");
            MonsterTradingCardsGame.Write(
                "[Back]",
                _menuSlotHorizontal == 2 && _menuSlotVertical == 1 ? ConsoleColor.Black : ConsoleColor.White,
                _menuSlotHorizontal == 2 && _menuSlotVertical == 1 ? ConsoleColor.White : ConsoleColor.Black
            );
        }
=======
>>>>>>> 856f5eb (Add login scene)
    }
}