using MonsterTradingCardsGame.Core.Input;
using MonsterTradingCardsGame.Core.UI;

namespace MonsterTradingCardsGame_Client.Scene.Scenes;

public class DashboardScene : IScene
{
    public void Update()
    {
        Input.WaitForKeyPress();
    }

    public void Destroy()
    {
    }

    public void Draw()
    {
        DrawStatBar();
    }

    private void DrawStatBar()
    {
        Gui.Write(" Coins: ", ConsoleColor.DarkYellow);       
        Gui.Write("999");
        Gui.SpaceHorizontal(8);
        Gui.Write("MMR: ", ConsoleColor.Cyan);       
        Gui.Write("999");
    }
}