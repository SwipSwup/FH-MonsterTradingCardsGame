using System;
using System.Linq;
using System.Reflection;

namespace MonsterTradingCardsGame.Core.UI;

public static class Gui
{
    public static void SpaceHorizontal(int width, GuiStyle style)
    {
        Write(string.Concat(Enumerable.Repeat(" ", width)), style.TextColor,
            style.BackgroundColor);
    }

    public static void SpaceHorizontal(int width)
    {
        SpaceHorizontal(width, new GuiStyle());
    }

    public static void SpaceVertical(int height, GuiStyle style)
    {
        Write(string.Concat(Enumerable.Repeat("\n", height)), style.TextColor, style.BackgroundColor);
    }

    public static void SpaceVertical(int height)
    {
        SpaceVertical(height, new GuiStyle());
    }

    public static void Button(string text, bool selected, GuiStyle style)
    {
        SpaceHorizontal(style.Offset);

        Write(
            $"[{text}]",
            selected ? style.SelectedTextColor : style.TextColor,
            selected ? style.SelectedBackgroundColor : style.BackgroundColor
        );
    }

    public static void Button(string text, bool selected)
    {
        Button(text, selected, new GuiStyle());
    }

    public static void TextField(string text, bool selected, int length, GuiStyle style)
    {
        SpaceHorizontal(style.Offset);
        WriteLine(
            $"[{text}{String.Concat(Enumerable.Repeat("_", length - text.Length))}]",
            selected ? style.SelectedTextColor : style.TextColor,
            selected ? style.SelectedBackgroundColor : style.BackgroundColor
        );
    }

    public static void TextField(string text, bool selected, int length)
    {
        TextField(text, selected, length, new GuiStyle());
    }
    
    public static void WriteLine(string message, ConsoleColor textColor = ConsoleColor.White,
        ConsoleColor backgroundColor = ConsoleColor.Black)
    {
        WriteInternal(message, textColor, backgroundColor, Console.WriteLine);
    }

    public static void Write(string message, ConsoleColor textColor = ConsoleColor.White,
        ConsoleColor backgroundColor = ConsoleColor.Black)
    {
        WriteInternal(message, textColor, backgroundColor, Console.Write);
    }

    private static void WriteInternal(string message, ConsoleColor textColor, ConsoleColor backgroundColor,
        Action<string> write)
    {
        Console.ForegroundColor = textColor;
        Console.BackgroundColor = backgroundColor;
        write(message);
        Console.ResetColor();
    }
}