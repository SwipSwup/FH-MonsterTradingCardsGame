namespace Client.Engine.Scene;

public interface IScene
{
    public void Init();

    public void Destroy();

    public void Update();

    public void Draw();
}