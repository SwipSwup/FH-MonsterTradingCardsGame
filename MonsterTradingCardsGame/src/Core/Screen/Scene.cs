using System;

namespace MonsterTradingCardsGame.Core.Screen
{
    public abstract class Scene
    {
        public virtual void Draw()
        {
            Console.Clear();
        }
        
        public abstract void Update();
    }
}