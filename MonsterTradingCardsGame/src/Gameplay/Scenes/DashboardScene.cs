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
        /*Gui.Write(" Coins: ", ConsoleColor.DarkYellow);       
        Gui.Write("999");
        Gui.SpaceHorizontal(8);
        Gui.Write("MMR: ", ConsoleColor.Cyan);       
        Gui.Write("999");*/
        
        Gui.DrawAt(CardAssets.CardBody, new Coord {X = 2, Y = 0});
        Gui.DrawAt(MonsterAssets.Wizard, new Coord {X = 3, Y = 1});
        Gui.DrawAt(CardAssets.CardBody, new Coord {X = 22, Y = 0});
        Gui.DrawAt(MonsterAssets.Knight, new Coord {X = 23, Y = 1});
        Gui.DrawAt(CardAssets.CardBody, new Coord {X = 42, Y = 0});
        Gui.DrawAt(MonsterAssets.Dragon, new Coord {X = 43, Y = 1});
        Gui.DrawAt(CardAssets.CardBody, new Coord {X = 62, Y = 0});
        Gui.DrawAt(MonsterAssets.Kraken, new Coord {X = 63, Y = 1});
        Gui.DrawAt(CardAssets.CardBody, new Coord {X = 82, Y = 0});
    }
}