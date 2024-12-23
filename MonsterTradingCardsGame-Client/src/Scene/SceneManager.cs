namespace MonsterTradingCardsGame_Client.Scene;

internal static class SceneManager
{
    public static IScene? ActiveScene { get; private set; }

    public static void LoadScene(IScene scene)
    {
        if(ActiveScene != null)
            UnloadActiveScene();
        
        ActiveScene = scene;
        ActiveScene.Initialize();
        ActiveScene.Draw();
    }

    public static void UnloadActiveScene()
    {
        ActiveScene?.Destroy();
        Console.Clear();
    }

    public static void UpdateActiveScene()
    {
        ActiveScene?.Update();
        ActiveScene?.Draw();
    }
}