using System;
using System.Collections.Generic;

namespace MonsterTradingCardsGame.Core.Input
{
    public static class Input
    {
        private static Dictionary<ConsoleKey, Action<ConsoleKeyInfo>> _registeredKeys = new();

        private static Action<ConsoleKeyInfo> _defaultAction;

        public static bool WaitForKeyPress()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (!_registeredKeys.ContainsKey(keyInfo.Key))
            {
                _defaultAction?.Invoke(keyInfo);
                return false;
            }


            _registeredKeys[keyInfo.Key]?.Invoke(keyInfo);
            return true;
        }

        public static void RegisterDefaultAction(Action<ConsoleKeyInfo> action) => _defaultAction += action;

        public static void UnregisterDefaultAction(Action<ConsoleKeyInfo> action) => _defaultAction -= action;

        public static void RegisterKeyAction(ConsoleKey key, Action<ConsoleKeyInfo> action)
        {
            if (_registeredKeys.TryGetValue(key, out var actions))
            {
                actions += action;
                _registeredKeys[key] = actions;
            }
            else
            {
                _registeredKeys.Add(key, action);
            }
        }

        public static void UnregisterKeyAction(ConsoleKey key, Action<ConsoleKeyInfo> action)
        {
            if (_registeredKeys.TryGetValue(key, out var actions))
            {
                actions -= action;
                _registeredKeys[key] = actions;
            }
        }

        public static void BuildString(ConsoleKeyInfo keyInfo, ref string target, int maxStringLength = int.MaxValue)
        {
            
            if (keyInfo.Key == ConsoleKey.Backspace)
            {
                target = target.Substring(0, target.Length - 1);
                return;
            }

            if(target.Length + 1 >= maxStringLength)
                return;

            if (keyInfo.Key >= ConsoleKey.A && keyInfo.Key <= ConsoleKey.Z ||
                keyInfo.Key >= ConsoleKey.D0 && keyInfo.Key <= ConsoleKey.D9)
                target += keyInfo.KeyChar;
        }
    }
}