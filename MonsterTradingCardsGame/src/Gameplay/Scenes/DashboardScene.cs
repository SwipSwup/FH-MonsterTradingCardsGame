using System;
using MonsterTradingCardsGame.Assets.Cards;
using MonsterTradingCardsGame.Core.Input;
using MonsterTradingCardsGame.Core.Scene;
using MonsterTradingCardsGame.Core.UI;

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
        
        Gui.DrawAt(CardAssets.CardBody, new Coord {X = 0, Y = 10});
        Gui.DrawAt(CardAssets.CardBody, new Coord {X = 20, Y = 10});
        Gui.DrawAt(CardAssets.CardBody, new Coord {X = 40, Y = 10});
        Gui.DrawAt(CardAssets.CardBody, new Coord {X = 60, Y = 10});
        Gui.DrawAt(CardAssets.CardBody, new Coord {X = 80, Y = 10});
    }
}