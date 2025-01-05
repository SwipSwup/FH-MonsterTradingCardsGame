using System.Runtime.InteropServices;

namespace Client.Engine.ConsoleManager;

public class ConsoleManager
{
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
    
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetConsoleWindowInfo(IntPtr hConsoleOutput, bool absolute, [In] ref SMALL_RECT consoleWindow);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, COORD size);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetConsoleCursorInfo(IntPtr hConsoleOutput, ref CONSOLE_CURSOR_INFO lpConsoleCursorInfo);

    [StructLayout(LayoutKind.Sequential)]
    struct CONSOLE_CURSOR_INFO
    {
        public uint dwSize;
        public bool bVisible;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct COORD
    {
        public short X;
        public short Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct SMALL_RECT
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }

    private const int STD_OUTPUT_HANDLE = -11;
    private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

    private IntPtr _stdHandle = IntPtr.Zero;

    public ConsoleManager()
    {
        InitConsole();
    }

    public ConsoleManager(string title, short width, short height)
    {
        InitConsole();

        ResizeConsole(width, height);

        Console.Title = title;
    }

    private void InitConsole()
    {
        _stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);

        EnableVirtualTerminalProcessing();
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        DisableCursor();
    }

    private void EnableVirtualTerminalProcessing()
    {
        if (!GetConsoleMode(_stdHandle, out uint mode))
        {
            //TODO Debug console
            Console.WriteLine("Error: Could not get console mode.");
            return;
        }

        mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;

        if (!SetConsoleMode(_stdHandle, mode))
        {
            //TODO Debug console
            Console.WriteLine("Error: Could not set console mode.");
        }
    }

    private void ResizeConsole(short width, short height)
    {
        SetConsoleScreenBufferSize(_stdHandle, new COORD { X = width, Y = height });

        SMALL_RECT rect = new SMALL_RECT
        {
            Left = 0,
            Top = 0,
            Right = (short)(width - 1),
            Bottom = (short)(height - 1)
        };
        SetConsoleWindowInfo(_stdHandle, true, ref rect);
    }

    void DisableCursor()
    {
        CONSOLE_CURSOR_INFO info = new CONSOLE_CURSOR_INFO { dwSize = 1, bVisible = false };
        SetConsoleCursorInfo(_stdHandle, ref info);
    }
}