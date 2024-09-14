using System;

namespace MonsterTradingCardsGame.Core.Scene
{
    public abstract class Scene
    {
        protected MonsterTradingCardsGame Game;


<<<<<<< Updated upstream
<<<<<<< HEAD
        public virtual void Initialize(Gameplay.MonsterTradingCardsGame game)
=======
        public void Initialize(Gameplay.MonsterTradingCardsGame game)
>>>>>>> 856f5eb (Add login scene)
=======
        public virtual void Initialize(MonsterTradingCardsGame game)
>>>>>>> Stashed changes
        {
            Game = game;
        }
        public virtual void Draw()
        {
            Console.Clear();
        }
        
        public abstract void Update();
<<<<<<< HEAD
        
        public abstract void Destroy();
=======
>>>>>>> 856f5eb (Add login scene)
    }
}