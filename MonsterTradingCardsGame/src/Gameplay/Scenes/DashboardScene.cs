using System;
using MonsterTradingCardsGame.Core.Input;
using MonsterTradingCardsGame.Core.Scene;
using MonsterTradingCardsGame.Core.UI;
using MonsterTradingCardsGame.Gameplay.Client;

namespace MonsterTradingCardsGame.Gameplay.Scenes;

public class DashboardScene : Scene
{
    public override void Update()
    {
        Input.WaitForKeyPress();
    }

    public override void Destroy()
    {
    }

    public override void Draw()
    {
        base.Draw();
        
        DrawStatBar();
    }

    private void DrawStatBar()
    {
        Gui.Write(" Coins: ", ConsoleColor.DarkYellow);       
        Gui.Write("999");
        Gui.SpaceHorizontal(8);
        Gui.Write("MMR: ", ConsoleColor.Cyan);       
        Gui.Write("999");
        
        Gui.SpaceHorizontal(8);
        Gui.Write(ServerClientManager.Token);
    }
}