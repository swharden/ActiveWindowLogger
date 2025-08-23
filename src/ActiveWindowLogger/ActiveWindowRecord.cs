using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ActiveWindowLogger;

public readonly record struct ActiveWindowRecord
{
    public required DateTime DateTime { get; init; }
    public TimeSpan Age => DateTime.Now - DateTime;
    public required string Path { get; init; }
    public required string Title { get; init; }
    public required int MouseX { get; init; }
    public required int MouseY { get; init; }
    public readonly string GetCsvLine() => $"{DateTime}, {Path}, {Title}";

    private struct Point
    {
        public int X;
        public int Y;
    }

    public static ActiveWindowRecord GetCurrentState()
    {
        var mouse = GetMousePosition();

        return new ActiveWindowRecord()
        {
            DateTime = DateTime.Now,
            Title = ActiveWindowTitle(),
            Path = GetActiveWindowExecutablePath(),
            MouseX = mouse.X,
            MouseY = mouse.Y,
        };
    }

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out Point lpPoint);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    private static string ActiveWindowTitle()
    {
        const int nChars = 256;
        StringBuilder buffer = new(nChars);
        IntPtr handle = GetForegroundWindow();

        string title = GetWindowText(handle, buffer, nChars) > 0
            ? buffer.ToString()
            : string.Empty;

        return title;
    }

    static string GetActiveWindowExecutablePath()
    {
        IntPtr hWnd = GetForegroundWindow();

        if (hWnd == IntPtr.Zero)
            return string.Empty;

        _ = GetWindowThreadProcessId(hWnd, out uint processId);

        try
        {
            Process process = Process.GetProcessById((int)processId);
            return process.MainModule?.FileName ?? string.Empty;
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    private static Point GetMousePosition()
    {
        GetCursorPos(out Point point);
        return point;
    }
}