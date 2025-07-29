using Microsoft.Win32;
using System.Diagnostics;

namespace ActiveWindowLogger.GUI;

public partial class Form1 : Form
{
    readonly Logger Monitor = new()
    {
        LogFolderPath = Path.GetDirectoryName(Application.ExecutablePath)!,
        CheckInterval = TimeSpan.FromSeconds(1),
    };

    readonly NotifyIcon NotifyIcon;

    public Form1()
    {
        InitializeComponent();
        Icon = Properties.Resources.icon;
        NotifyIcon = GetNotifyIcon();

        rtbLogs.Clear();
        lblFolder.Text = Monitor.LogFolderPath;

        btnStart.Click += (s, e) => StartLogging();
        btnStop.Click += (s, e) => StopLogging();
        btnSetFolder.Click += (s, e) => SetFolder();
        btnLaunchFolder.Click += (s, e) => LaunchFolder();

        Load += (s, e) =>
        {
            Monitor.LineLogged += (s, e) => rtbLogs.Invoke(new Action(() => LogLine(e)));
            StartLogging();
        };
        HideWindow();
    }


    private NotifyIcon GetNotifyIcon()
    {
        ToolStripMenuItem startupMenuItem = new("Start with Windows")
        {
            CheckOnClick = true,
            Checked = IsStartupEnabled(),
        };
        startupMenuItem.CheckedChanged += (s, e) => SetStartup(startupMenuItem.Checked);

        ContextMenuStrip menu = new();
        menu.Items.Add("Open", null, (s, e) => ShowWindow());
        menu.Items.Add(startupMenuItem);
        menu.Items.Add("Exit", null, (s, e) => Application.Exit());

        NotifyIcon notify = new()
        {
            Icon = Properties.Resources.icon,
            Text = "Active Window Logger",
            Visible = true,
            ContextMenuStrip = menu,
        };

        notify.MouseClick += (s, e) =>
        {
            if (e.Button == MouseButtons.Left)
                ShowWindow();
        };

        return notify;
    }

    public const string APP_NAME = "ActiveWindowLogger";
    private static RegistryKey GetRegistryKey(bool writable)
    {
        return Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", writable) 
            ?? throw new NullReferenceException("access to startup registry path failed");
    }

    public static bool IsStartupEnabled()
    {
        using RegistryKey key = GetRegistryKey(false);
        string? currentValue = key?.GetValue(APP_NAME) as string;
        bool isEnabled = currentValue is not null && currentValue.Contains(Application.ExecutablePath);
        Debug.WriteLine($"Current registry startup enabled: {isEnabled}");
        return isEnabled;
    }

    public static void SetStartup(bool enable)
    {
        using RegistryKey key = GetRegistryKey(true);
        Debug.WriteLine($"Setting registry startup: {enable}");
        if (enable)
            key.SetValue(APP_NAME, $"\"{Application.ExecutablePath}\"");
        else
            key.DeleteValue(APP_NAME, false);
    }

    private void ShowWindow()
    {
        Show();
        WindowState = FormWindowState.Normal;
        ShowInTaskbar = true;
        BringToFront();
    }

    private void HideWindow()
    {
        WindowState = FormWindowState.Minimized;
        ShowInTaskbar = false;
        Hide();
    }

    private void SetFolder()
    {
        FolderBrowserDialog dialog = new();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            Monitor.LogFolderPath = dialog.SelectedPath;
            lblFolder.Text = Monitor.LogFolderPath;
        }
    }

    private void LaunchFolder()
    {
        System.Diagnostics.Process.Start("explorer.exe", Monitor.LogFolderPath);
    }

    private void LogLine(string line)
    {
        rtbLogs.AppendText(line);
        rtbLogs.SelectionStart = rtbLogs.TextLength;
        rtbLogs.ScrollToCaret();
    }

    private void StartLogging()
    {
        btnStart.Enabled = false;
        btnStop.Enabled = true;
        Monitor.Start();
    }

    private void StopLogging()
    {
        btnStart.Enabled = true;
        btnStop.Enabled = false;
        Monitor.Stop();
    }
}
