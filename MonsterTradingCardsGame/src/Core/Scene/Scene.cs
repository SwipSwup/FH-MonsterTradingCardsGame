using System;

namespace MonsterTradingCardsGame.Core.Scene
{
    public abstract class Scene
    {
        protected Gameplay.MonsterTradingCardsGame Game;


        public virtual void Initialize(Gameplay.MonsterTradingCardsGame game)
        {
            Game = game;
        }
        public virtual void Draw()
        {
            Console.Clear();
        }
        
        public abstract void Update();
        
        public abstract void Destroy();
    }
}