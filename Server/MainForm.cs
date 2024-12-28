using System;
using System.Configuration;
using System.Windows.Forms;
using static System.Windows.Forms.MonthCalendar;

public partial class MainForm : Form
{
    internal Listener _listener;

    public MainForm()
    {
        InitializeComponent();
        StartServer();
    }

    internal void ReceiveMessage(string userName, string text, string description, string hostName)
    {
        this.Invoke(new Action(() =>
        {
            chatArea.Text += $"Пользователь: {userName} Хост: {hostName} Информация: {description}\r\n";
            chatArea.Text += $"{text}\r\n";
        }));
    }

    private void startMenuButton_Click(object sender, EventArgs e)
    {
        StartServer();
    }

    private void StartServer()
    {
        var url = ConfigurationManager.AppSettings["url"];
        _listener = new Listener(url);
        _listener.actionReceiveMessage += ReceiveMessage;
        _listener.Start();
        startMenuButton.Enabled = false;
        stopMenuButton.Enabled = true;
    }

    private void StopServer()
    {
        startMenuButton.Enabled = true;
        stopMenuButton.Enabled = false;
        _listener.Stop();
    }

    private void stopMenuButton_Click(object sender, EventArgs e)
    {
        StopServer();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        StopServer();
    }
}