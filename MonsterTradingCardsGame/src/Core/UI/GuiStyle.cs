using System;

namespace MonsterTradingCardsGame.Core.UI;

public struct GuiStyle
{
    public int Offset;
    
    public ConsoleColor TextColor;
    public ConsoleColor BackgroundColor;
    public ConsoleColor SelectedTextColor;
    public ConsoleColor SelectedBackgroundColor;

    public GuiStyle()
    {
        Offset = 0;
        TextColor = ConsoleColor.White;
        BackgroundColor = ConsoleColor.Black;
        SelectedTextColor = ConsoleColor.Black;
        SelectedBackgroundColor = ConsoleColor.White;
    }
}