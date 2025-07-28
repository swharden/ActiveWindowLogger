using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ActiveWindowLogger;

public class StateMonitor
{
    public TimeSpan InactiveThreshold { get; set; } = TimeSpan.FromMinutes(5);
    public TimeSpan CheckInterval { get; set; } = TimeSpan.FromSeconds(5);
    public EventHandler<string>? LineLogged { get; set; }

    readonly string LogFolderPath;
    string WindowLast = string.Empty;
    string MouseLast = string.Empty;
    bool IsActive = true;
    readonly Stopwatch LastActivityTimer = Stopwatch.StartNew();

    public StateMonitor(string logFolderPath = "./")
    {
        logFolderPath = Path.GetFullPath(logFolderPath);
        if (!Directory.Exists(logFolderPath))
        {
            string? parent = Path.GetDirectoryName(logFolderPath);
            if (parent is null || !Directory.Exists(parent))
            {
                throw new DirectoryNotFoundException(parent);
            }
            Directory.CreateDirectory(logFolderPath);
        }
        LogFolderPath = logFolderPath;
    }

    private string LogFilePath => Path.Join(LogFolderPath,
        $"{DateTime.Now.Year}-{DateTime.Now.Month:00}-{DateTime.Now.Day:00}.txt");

    private void Log(string message)
    {
        string line = $"{DateTime.Now}, {message}\n";
        File.AppendAllText(LogFilePath, line);
        LineLogged?.Invoke(this, line);
    }

    public void RunForever()
    {
        Log($"ActiveWindowLogger - Started");
        Log($"ActiveWindowLogger - Log Folder: {LogFolderPath}");
        Log($"ActiveWindowLogger - Check Interval Seconds: {CheckInterval.TotalSeconds}");
        Log($"ActiveWindowLogger - Inactivity Threshold Seconds: {InactiveThreshold.TotalSeconds}");
        while (true)
        {
            Update();
            Thread.Sleep(CheckInterval);
        }
    }

    private void Update()
    {
        bool isNewActivity = false;

        string windowTitle = GetWindowTitle();
        if (windowTitle != WindowLast)
        {
            WindowLast = windowTitle;
            Log(windowTitle);
            isNewActivity = true;
        }

        string mousePosition = GetMousePosition();
        if (mousePosition != MouseLast)
        {
            MouseLast = mousePosition;
            isNewActivity = true;
        }

        if (isNewActivity)
        {
            if (IsActive == false)
            {
                Log(windowTitle);
            }
            IsActive = true;
            LastActivityTimer.Restart();
        }
        else
        {
            if (IsActive && LastActivityTimer.Elapsed >= InactiveThreshold)
            {
                IsActive = false;
                Log($"ActiveWindowLogger - Inactive");
            }
        }
    }

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out Point lpPoint);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    private struct Point { public int X; public int Y; }
    private static string GetMousePosition()
    {
        GetCursorPos(out Point point);
        return $"{point.X},{point.Y}";
    }

    private static string GetWindowTitle()
    {
        const int nChars = 256;
        StringBuilder buffer = new(nChars);
        IntPtr handle = GetForegroundWindow();

        string title = GetWindowText(handle, buffer, nChars) > 0
            ? buffer.ToString()
            : string.Empty;

        return title;
    }
}
