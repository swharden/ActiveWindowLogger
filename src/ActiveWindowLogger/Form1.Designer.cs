namespace ActiveWindowLogger;

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
        groupBox3 = new GroupBox();
        panel1 = new Panel();
        checkStartWithWindows = new CheckBox();
        btnViewLogs = new Button();
        groupBox1 = new GroupBox();
        label1 = new Label();
        groupBox3.SuspendLayout();
        groupBox1.SuspendLayout();
        SuspendLayout();
        // 
        // groupBox3
        // 
        groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        groupBox3.Controls.Add(panel1);
        groupBox3.Location = new Point(12, 87);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(1355, 546);
        groupBox3.TabIndex = 5;
        groupBox3.TabStop = false;
        groupBox3.Text = "Recent Logs";
        // 
        // panel1
        // 
        panel1.Dock = DockStyle.Fill;
        panel1.Location = new Point(3, 19);
        panel1.Name = "panel1";
        panel1.Size = new Size(1349, 524);
        panel1.TabIndex = 0;
        // 
        // checkStartWithWindows
        // 
        checkStartWithWindows.AutoSize = true;
        checkStartWithWindows.Location = new Point(97, 14);
        checkStartWithWindows.Name = "checkStartWithWindows";
        checkStartWithWindows.Size = new Size(128, 19);
        checkStartWithWindows.TabIndex = 4;
        checkStartWithWindows.Text = "Start with Windows";
        checkStartWithWindows.UseVisualStyleBackColor = true;
        // 
        // btnViewLogs
        // 
        btnViewLogs.Location = new Point(15, 11);
        btnViewLogs.Name = "btnViewLogs";
        btnViewLogs.Size = new Size(76, 23);
        btnViewLogs.TabIndex = 6;
        btnViewLogs.Text = "View Logs";
        btnViewLogs.UseVisualStyleBackColor = true;
        // 
        // groupBox1
        // 
        groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new Point(12, 40);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(1355, 41);
        groupBox1.TabIndex = 7;
        groupBox1.TabStop = false;
        groupBox1.Text = "Active Window";
        // 
        // label1
        // 
        label1.Dock = DockStyle.Fill;
        label1.Location = new Point(3, 19);
        label1.Name = "label1";
        label1.Size = new Size(1349, 19);
        label1.TabIndex = 0;
        label1.Text = "label1";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1379, 645);
        Controls.Add(groupBox1);
        Controls.Add(btnViewLogs);
        Controls.Add(checkStartWithWindows);
        Controls.Add(groupBox3);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Active Window Logger";
        groupBox3.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private GroupBox groupBox3;
    private CheckBox checkStartWithWindows;
    private Button btnViewLogs;
    private Panel panel1;
    private GroupBox groupBox1;
    private Label label1;
}
