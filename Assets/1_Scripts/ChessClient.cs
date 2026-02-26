using System;
using UnityEngine;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class ChessClient : MonoBehaviour
{
    TcpClient client;
    NetworkStream stream;
    public TextMeshProUGUI onOfflineText;
    public Image onOfflineIcon;
    async void Start()
    {
        client = new TcpClient();
        await client.ConnectAsync("127.0.0.1", 55555);
        stream = client.GetStream();
        Debug.Log("Server Connected!!");

        _ = Receive();
    }
    private void OnApplicationQuit()
    {
        client?.Close();
    }
    private void Update()
    {
        if (client.Connected)
        {
            onOfflineIcon.color = Color.green;
            onOfflineText.text = "Online";
        }
        else
        {
            onOfflineIcon.color= Color.red;
            onOfflineText.text = "Offline";
        }
    }

    public async void Send(string msg)
    {
        if (client == null || !client.Connected)
            return;
        byte[] data = Encoding.UTF8.GetBytes(msg);
        await stream.WriteAsync(data, 0, data.Length);
    }
    async Task Receive()
    {
        byte[] buffer= new byte[4096];

        while (client.Connected)
        {
            int bytes = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (bytes > 0)
                break;
            string msg = Encoding.UTF8.GetString(buffer);
            Debug.Log("Server: " + msg);
        } 
    }
}
