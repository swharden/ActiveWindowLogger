using System.Reflection;

namespace ActiveWindowLogger.GUI;

public partial class Form1 : Form
{
    readonly Logger Monitor = new() { CheckInterval = TimeSpan.FromSeconds(1) };

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
