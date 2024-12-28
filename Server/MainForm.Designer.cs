partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Button startMenuButton;
    private System.Windows.Forms.Button stopMenuButton;
    private System.Windows.Forms.TextBox chatArea;

    private void InitializeComponent()
    {
        this.startMenuButton = new System.Windows.Forms.Button();
        this.stopMenuButton = new System.Windows.Forms.Button();
        this.chatArea = new System.Windows.Forms.TextBox();
        this.SuspendLayout();

        
        this.startMenuButton.Location = new System.Drawing.Point(12, 12);
        this.startMenuButton.Name = "startMenuButton";
        this.startMenuButton.Size = new System.Drawing.Size(75, 23);
        this.startMenuButton.TabIndex = 0;
        this.startMenuButton.Text = "Start Server";
        this.startMenuButton.UseVisualStyleBackColor = true;
        this.startMenuButton.Click += new System.EventHandler(this.startMenuButton_Click);

        
        this.stopMenuButton.Location = new System.Drawing.Point(93, 12);
        this.stopMenuButton.Name = "stopMenuButton";
        this.stopMenuButton.Size = new System.Drawing.Size(75, 23);
        this.stopMenuButton.TabIndex = 1;
        this.stopMenuButton.Text = "Stop Server";
        this.stopMenuButton.UseVisualStyleBackColor = true;
        this.stopMenuButton.Click += new System.EventHandler(this.stopMenuButton_Click);

        
        this.chatArea.Location = new System.Drawing.Point(12, 41);
        this.chatArea.Multiline = true;
        this.chatArea.Name = "chatArea";
        this.chatArea.Size = new System.Drawing.Size(260, 208);
        this.chatArea.TabIndex = 2;

        
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(284, 261);
        this.Controls.Add(this.chatArea);
        this.Controls.Add(this.stopMenuButton);
        this.Controls.Add(this.startMenuButton);
        this.Name = "MainForm";
        this.Text = "MainForm";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }
}