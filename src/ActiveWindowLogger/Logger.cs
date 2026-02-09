using System.Diagnostics;

namespace ActiveWindowLogger;

public class Logger
{
    public static string Version { get; } = "1.1";
    public TimeSpan InactiveThreshold { get; set; } = TimeSpan.FromMinutes(5);
    public TimeSpan CheckInterval { get; set; } = TimeSpan.FromSeconds(5);
    public EventHandler<string>? LineLogged { get; set; }
    public EventHandler<ActiveWindowRecord>? ActiveWindowChecked { get; set; }
    readonly System.Timers.Timer Timer = new();
    readonly CloudTimeTracker CloudTimeTracker = new();

    public string LogFolder { get; set; } = Path.GetFullPath("./");
    ActiveWindowRecord LastChangedState;
    readonly Stopwatch LastActivityTimer = Stopwatch.StartNew();

    private string LogFilePath => Path.Join(LogFolder,
        $"{DateTime.Now.Year}-{DateTime.Now.Month:00}-{DateTime.Now.Day:00}.txt");

    public Logger()
    {
        Timer.Elapsed += (s, e) => Update();
    }

    private void LogMessage(string key, string value)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        string line = $"{timestamp}, {key}, {value}\n";
        File.AppendAllText(LogFilePath, line);
        LineLogged?.Invoke(this, line);
    }

    private void LogWindowActivity(ActiveWindowRecord window)
    {
        LogMessage(window.Path, window.Title);
    }

    private void LogWentInactive()
    {
        string key = "ActiveWindowLogger";
        LogMessage(key, $"Inactive for: {InactiveThreshold.Seconds}");
    }

    private void LogStart()
    {
        string key = "ActiveWindowLogger";
        LogMessage(key, $"Started");
        LogMessage(key, $"Log Folder: {LogFolder}");
        LogMessage(key, $"Check Interval Seconds: {CheckInterval.TotalSeconds}");
        LogMessage(key, $"Inactivity Threshold Seconds: {InactiveThreshold.TotalSeconds}");
    }

    public void Start()
    {
        LogStart();
        Update();
        Timer.Interval = CheckInterval.TotalMilliseconds;
        Timer.Start();
    }

    bool IsInactive = false;
    private void Update()
    {
        ActiveWindowRecord currentState = ActiveWindowRecord.GetCurrentState();
        ActiveWindowChecked?.Invoke(this, currentState);

        bool nothingChanged = currentState.Title == LastChangedState.Title &&
            currentState.MouseX == LastChangedState.MouseX &&
            currentState.MouseY == LastChangedState.MouseY;

        if (nothingChanged)
        {
            if (IsInactive == false && LastChangedState.Age >= InactiveThreshold)
            {
                IsInactive = true;
                LogWentInactive();
            }
            return;
        }

        if (currentState.Title != LastChangedState.Title || IsInactive)
        {
            LogWindowActivity(currentState);
        }

        if (!nothingChanged)
        {
            // Fire and forget. Don't track at all. Let it fail.
            Task.Run(async () =>
            {
                await CloudTimeTracker.LogActivity();
            });
        }

        IsInactive = false;
        LastChangedState = currentState;
    }
}
