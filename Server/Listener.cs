using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Listener
{
    public Action<string, string, string, string> actionReceiveMessage;
    internal ASR _asr = null;
    internal HttpListener _listener;
    internal string Url { get; set; }
    internal bool _workingToken = false;

    public static string pageData =
        "<!DOCTYPE>" +
        "<html>" +
        "  <head>" +
        "    <title>Сервер приема аудиосообщений</title>" +
        "    <meta charset=\"UTF-8\">" +
        "  </head>" +
        "  <body>" +
        "    <h1>Сервер приема аудиосообщений</h1>" +
        "  </body>" +
        "</html>";

    public Listener(string url)
    {
        Url = url;
        _asr = new ASR();
        _asr.LoadModelsUsers();
    }

    public void Start()
    {
        _workingToken = true;
        _listener = new HttpListener();
        _listener.Prefixes.Add(Url);
        _listener.Start();
        HandleIncomingConnections();
    }

    public void Stop()
    {
        _workingToken = false;
        if (_listener != null) _listener.Stop();
        _listener = null;
    }

    public async Task HandleIncomingConnections()
    {
        while (_workingToken)
        {
            HttpListenerContext ctx = await _listener.GetContextAsync();
            HttpListenerRequest req = ctx.Request;
            HttpListenerResponse resp = ctx.Response;
            byte[] data = new byte[0];
            try
            {
                if (req.HttpMethod == "POST")
                {
                    var inputBuffer = new byte[50 * 1024];
                    var len = await req.InputStream.ReadAsync(inputBuffer, 0, inputBuffer.Length);
                    var message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(Encoding.UTF8.GetString(inputBuffer));
                    var description = string.Empty;
                    if (_asr != null && message.Dictor != null)
                    {
                        var resultSpeaker = _asr.GetSpeaker(message.Dictor);
                        if (resultSpeaker != null) description = $"диктор - {resultSpeaker.Name}, точность - {resultSpeaker.Accuracy}";
                    }
                    if (actionReceiveMessage != null) actionReceiveMessage(message.Name, message.Text, description, message.HostName);
                    data = Encoding.UTF8.GetBytes("Ok");
                    resp.ContentType = "application/json";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;
                }
                else if (req.HttpMethod == "GET")
                {
                    data = Encoding.UTF8.GetBytes(pageData);
                    resp.ContentType = "text/html";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;
                }
            }
            catch (Exception ex)
            {
            }
            await resp.OutputStream.WriteAsync(data, 0, data.Length);
            resp.Close();
        }
    }
}