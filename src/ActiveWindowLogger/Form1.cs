using Microsoft.Win32;
using System.Diagnostics;

namespace ActiveWindowLogger;

public partial class Form1 : Form
{
    readonly Logger Monitor = new();

    readonly NotifyIcon NotifyIcon;

    readonly string LogFolder = Path.Join(Path.GetDirectoryName(Application.ExecutablePath), "logs");

    public Form1()
    {
        InitializeComponent();

        Icon = Properties.Resources.icon;
        NotifyIcon = GetNotifyIcon();
        checkStartWithWindows.Checked = IsStartupEnabled();

        btnViewLogs.Click += (s, e) => LaunchFolder();
        checkStartWithWindows.CheckedChanged += (s, e) => SetStartup(checkStartWithWindows.Checked);

        Load += (s, e) =>
        {
            if (!Directory.Exists(LogFolder))
                Directory.CreateDirectory(LogFolder);
            Monitor.LogFolder = LogFolder;
            Monitor.CheckInterval = TimeSpan.FromSeconds(1); // TODO: back this off if needed
            Monitor.LineLogged += (s, e) => Invoke(new Action(() => LogLine(e)));
            Monitor.Start();
        };

        FormClosing += (s, e) =>
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide(); // don't exit, just go to taskbar
            }
        };

        if (!Debugger.IsAttached)
        {
            HideWindow();
        }
    }

    private NotifyIcon GetNotifyIcon()
    {
        ContextMenuStrip menu = new();
        menu.Items.Add("Open", null, (s, e) => ShowWindow());
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

    private void LaunchFolder()
    {
        Process.Start("explorer.exe", Monitor.LogFolder);
    }

    private void LogLine(string line, int maxLines = 50)
    {
        Label lbl = new()
        {
            Text = line.Trim(),
            AutoSize = false,
            Height = 20,
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.TopLeft,
            AutoEllipsis = true,
        };

        panel1.Controls.Add(lbl);
        while (panel1.Controls.Count > maxLines)
        {
            panel1.Controls.RemoveAt(0);
        }
    }
}
