namespace ActiveWindowLogger.GUI;

public partial class Form1 : Form
{
    readonly StateMonitor Monitor = new() { CheckInterval = TimeSpan.FromSeconds(1) };

    public Form1()
    {
        InitializeComponent();
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
