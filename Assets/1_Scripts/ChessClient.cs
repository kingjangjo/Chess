using System;
using UnityEngine;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class roomInfo
{
    public string roomName { get; set; }
    public int playerCount { get; set; }
    public string roomId { get; set; }
}
public class ChessClient : MonoBehaviour
{
    public static ChessClient Instance { get; private set; }
    public TcpClient client { get; private set; }
    NetworkStream stream;
    public TextMeshProUGUI onOfflineText;
    public Image onOfflineIcon;
    public TMP_InputField roomNameInput;
    public TMP_InputField playerNameInput;
    public GameObject roomPrefab;
    public GameObject roomListObj;
    public List<roomInfo> roomList = new List<roomInfo>();
    StringBuilder sb = new StringBuilder();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);

    }
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
        if (stream == null) Debug.LogError("Stream is null");
        Debug.Log("[CLIENT SEND] " + msg);
        msg += "\n";
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
            if (bytes <= 0)
                break;
            string chunk = Encoding.UTF8.GetString(buffer, 0, bytes);
            sb.Append(chunk);
            while (true)
            {
                string current = sb.ToString();
                int index = current.IndexOf('\n');
                if (index == -1) break;

                string packet = current.Substring(0, index);
                sb.Remove(0, index + 1);
                HandlePacket(packet);
            }
        } 
    }
    void HandlePacket(string msg)
    {
        Debug.Log("Server: " + msg);
        var packet = msg.Split('|');
        switch (packet[0])
        {
            case "NEW_ROOM":
                {
                    roomList.Add(new roomInfo
                    {
                        roomId = packet[1],
                        roomName = packet[2],
                        playerCount = 0
                    });
                    var roomObj = Instantiate(roomPrefab, roomListObj.transform);
                    roomObj.GetComponent<RoomUi>().roomIndex = FindRoomIndex(packet[1]);
                    EnterRoom(packet[2], playerNameInput.text);
                    break;
                }
            case "PLAYER_ENTERED":
                {
                    foreach (var room in roomList)
                    {
                        if (room.roomId == packet[1])
                        {
                            room.playerCount++;
                            break;
                        }
                    }
                    break;
                }
            case "PIECE_MOVE":
                {
                    Vector2Int from = StringToVector2Int(packet[1]);
                    Vector2Int to = StringToVector2Int(packet[2]);
                    switch (BoardManager.Instance.IsBlocked(to.x, to.y))
                    {
                        case Condition.Piece:
                            {
                                BoardManager.Instance.CatchPiece(BoardManager.Instance.pieceBoard[to.x, to.y].gameObject);
                                break;
                            }
                        case Condition.Empty:
                            {
                                break;
                            }
                        case Condition.Out:
                            break;
                        default:
                            break;
                    }
                    Piece piece = BoardManager.Instance.pieceBoard[from.x, from.y];
                    BoardManager.Instance.OponentMovePos(from, to);
                    piece.transform.position = new Vector3(6.75f - (to.x * 1.5f), 0, 6.75f - (to.y * 1.5f));
                    TurnManager.instance.ChangeTurn();
                    Debug.Log($"Piece moved from {from} to {to}");
                    break;
                }
            case "ERROR":
                {
                    Debug.LogError("Server Error: " + packet[1]);
                    break;
                }
            case "LOG":
                {
                    Debug.Log("Server Log: " + packet[1]);
                    if (packet[1] == "YOU_ENTERED")
                    {
                        SceneManager.LoadScene("Match");
                    }
                    break;
                }
            default:
                {
                    Debug.LogError("Error: " + packet[1]);
                    break;
                }
        }
    }
    public Vector2Int StringToVector2Int(string s)
    {
        string trimmedString = s.Substring(1, s.Length - 2);

        string[] array = trimmedString.Split(", ");

        int x = int.Parse(array[0]);
        int y = int.Parse(array[1]);

        return new Vector2Int(x, y);
    }
    public int FindRoomIndex(string roomId)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].roomId == roomId)
                return i;
        }
        return -1;
    }
    public async void CreateRoom(string roomName)
    {
        Send($"CREATE_ROOM|{roomName}");
    }
    public async void EnterRoom(string roomName, string playerName)
    {
        Send($"ENTER_ROOM|{roomName}|{playerName}");
    }
    public void PushCreateRoomButton()
    {
        Debug.Log("Create Room Button Clicked");
        CreateRoom(roomNameInput.text);
    }
    public void MoveSend(Vector2Int from, Vector2Int to)
    {
         Send($"PIECE_MOVE|{playerNameInput.text}|{from}|{to}");
    }
}