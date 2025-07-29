namespace ActiveWindowLogger.GUI;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        lblFolder = new Label();
        btnSetFolder = new Button();
        btnLaunchFolder = new Button();
        groupBox1 = new GroupBox();
        groupBox2 = new GroupBox();
        btnStop = new Button();
        btnStart = new Button();
        groupBox3 = new GroupBox();
        rtbLogs = new RichTextBox();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox3.SuspendLayout();
        SuspendLayout();
        // 
        // lblFolder
        // 
        lblFolder.AutoSize = true;
        lblFolder.Location = new Point(123, 26);
        lblFolder.Name = "lblFolder";
        lblFolder.Size = new Size(22, 15);
        lblFolder.TabIndex = 0;
        lblFolder.Text = "???";
        // 
        // btnSetFolder
        // 
        btnSetFolder.Location = new Point(6, 22);
        btnSetFolder.Name = "btnSetFolder";
        btnSetFolder.Size = new Size(54, 23);
        btnSetFolder.TabIndex = 1;
        btnSetFolder.Text = "Set";
        btnSetFolder.UseVisualStyleBackColor = true;
        // 
        // btnLaunchFolder
        // 
        btnLaunchFolder.Location = new Point(63, 22);
        btnLaunchFolder.Name = "btnLaunchFolder";
        btnLaunchFolder.Size = new Size(54, 23);
        btnLaunchFolder.TabIndex = 2;
        btnLaunchFolder.Text = "Launch";
        btnLaunchFolder.UseVisualStyleBackColor = true;
        // 
        // groupBox1
        // 
        groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupBox1.Controls.Add(btnLaunchFolder);
        groupBox1.Controls.Add(btnSetFolder);
        groupBox1.Controls.Add(lblFolder);
        groupBox1.Location = new Point(146, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(642, 53);
        groupBox1.TabIndex = 3;
        groupBox1.TabStop = false;
        groupBox1.Text = "Log Folder";
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(btnStop);
        groupBox2.Controls.Add(btnStart);
        groupBox2.Location = new Point(12, 12);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(128, 53);
        groupBox2.TabIndex = 4;
        groupBox2.TabStop = false;
        groupBox2.Text = "Logger";
        // 
        // btnStop
        // 
        btnStop.Location = new Point(66, 22);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(54, 23);
        btnStop.TabIndex = 3;
        btnStop.Text = "Stop";
        btnStop.UseVisualStyleBackColor = true;
        // 
        // btnStart
        // 
        btnStart.Location = new Point(6, 22);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(54, 23);
        btnStart.TabIndex = 2;
        btnStart.Text = "Start";
        btnStart.UseVisualStyleBackColor = true;
        // 
        // groupBox3
        // 
        groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        groupBox3.Controls.Add(rtbLogs);
        groupBox3.Location = new Point(12, 71);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(776, 367);
        groupBox3.TabIndex = 5;
        groupBox3.TabStop = false;
        groupBox3.Text = "Logs";
        // 
        // rtbLogs
        // 
        rtbLogs.BackColor = SystemColors.Control;
        rtbLogs.BorderStyle = BorderStyle.None;
        rtbLogs.Dock = DockStyle.Fill;
        rtbLogs.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        rtbLogs.Location = new Point(3, 19);
        rtbLogs.Name = "rtbLogs";
        rtbLogs.ReadOnly = true;
        rtbLogs.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
        rtbLogs.Size = new Size(770, 345);
        rtbLogs.TabIndex = 0;
        rtbLogs.Text = resources.GetString("rtbLogs.Text");
        rtbLogs.WordWrap = false;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(groupBox3);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Active Window Logger";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Label lblFolder;
    private Button btnSetFolder;
    private Button btnLaunchFolder;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private Button btnStop;
    private Button btnStart;
    private GroupBox groupBox3;
    private RichTextBox rtbLogs;
}
