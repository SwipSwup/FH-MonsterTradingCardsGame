namespace MonsterTradingCardsGame_Client.Scene;

public interface IScene
{
    public virtual void Update()
    {
        Draw();
    }

    protected abstract void Draw();
}