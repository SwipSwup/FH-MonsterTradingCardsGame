namespace MonsterTradingCardsGame_Client.Scene
{
    public interface IScene
    {
        public void Initialize()
        {
            
        }

        public void Draw()
        {
            Console.Clear();
        }

        public void Update();

        public void Destroy();
    }
}