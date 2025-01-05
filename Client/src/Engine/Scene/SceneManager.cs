namespace Client.Engine.Scene;

public class SceneManager
{
    public static IScene? ActiveScene { get; private set; }

    public static void LoadScene(IScene scene)
    {
        ActiveScene = scene;
        scene.Init();
    }

    public static void UnloadScene()
    {
        if(ActiveScene == null)
            return;
        
        ActiveScene.Destroy();
        
        ActiveScene = null;
    }
}