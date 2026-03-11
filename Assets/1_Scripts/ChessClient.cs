using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Concurrent;

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
    public string roomId;
    StringBuilder sb = new StringBuilder();
    ConcurrentQueue<string> packetQueue = new ConcurrentQueue<string>();
    public bool gameEnded = false;
    public string turn;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
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
        try
        {
            client = new TcpClient();
            await client.ConnectAsync("172.28.5.81", 55555);
            stream = client.GetStream();
            Debug.Log("Server Connected!!");

            _ = Receive();
        }
        catch(Exception ex)
        {
            Debug.LogError($"Error:{ex}");
        }
        finally
        {

            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);

        }
    }
    private void OnApplicationQuit()
    {
        client?.Close();
    }
    private void Update()
    {
        while (packetQueue.TryDequeue(out string packet))
        {
            HandlePacket(packet);
        }
        if (SceneManager.GetActiveScene().name == "Loby")
        {
            onOfflineIcon = GameObject.FindWithTag("OnOfflineIcon").GetComponent<Image>();
            onOfflineText = GameObject.FindWithTag("OnOfflineText").GetComponent<TextMeshProUGUI>();
            if (client.Connected)
            {
                onOfflineIcon.color = Color.green;
                onOfflineText.text = "Online";
            }
            else
            {
                onOfflineIcon.color = Color.red;
                onOfflineText.text = "Offline";
            }
        }
    }
    async void CheckConnectionAfterSceneLoad()
    {
        if (stream == null || !stream.CanRead || !stream.CanWrite)
        {
            Debug.LogWarning("Stream dead. Reconnecting...");
            await Reconnect();
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Loby")
        {
            Reload();
            GameObject canvasObj = GameObject.FindWithTag("Canvas");
            if (canvasObj != null)
            {
                Button[] burttons = canvasObj.GetComponentsInChildren<Button>(true);
                foreach (Button b in burttons)
                {
                    if(b.name == "CreateButton")
                    {
                        b.onClick.RemoveAllListeners();
                        b.onClick.AddListener(PushCreateRoomButton);
                    }
                    else if(b.name == "ReloadButton")
                    {
                        b.onClick.RemoveAllListeners();
                        b.onClick.AddListener(Reload);
                    }
                }
                GridLayoutGroup[] gridLayoutGroups = canvasObj.GetComponentsInChildren<GridLayoutGroup>(true);
                foreach(GridLayoutGroup g in gridLayoutGroups)
                {
                    if(g.name == "RoomList")
                    {
                        roomListObj = g.gameObject;
                    }
                }
                TMP_InputField[] inputFields = canvasObj.GetComponentsInChildren<TMP_InputField>(true);
                foreach(var input in inputFields)
                {
                    if (input.name == "RoomNameInputField")
                    {
                        roomNameInput = input;
                    }
                    else if (input.name == "PlayerNameInputField")
                    {
                        playerNameInput = input;
                    }
                }
            }
        }
    }
    public void Send(string msg)
    {
       _ = SendMessage(msg);
    }
    public async Task SendMessage(string msg)
    {
        Debug.Log("Client connected: " + client.Connected);
        Debug.Log("Stream CanWrite: " + stream?.CanWrite);
        if (stream == null) Debug.LogError("Stream is null");
        try
        {
            Debug.Log("[CLIENT SEND] " + msg);
            msg += "\n";
            byte[] data = Encoding.UTF8.GetBytes(msg);
            await stream.WriteAsync(data, 0, data.Length);
        }
        catch (Exception e)
        {
            Debug.LogError("[SEND ERROR] " + e.Message);
        }
        //Debug.Log("[CLIENT SEND] " + msg);
        //msg += "\n";
        //if (client == null || !client.Connected)
        //    return;
        //byte[] data = Encoding.UTF8.GetBytes(msg);
        //await stream.WriteAsync(data, 0, data.Length);

    }
    async Task Receive()
    {
        Debug.LogWarning("Receive START");
        byte[] buffer = new byte[4096];

        try
        {
            while (true)
            {
                int bytes = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytes <= 0)
                {
                    Debug.Log("Receive closed (0 bytes)");
                    break;
                }
                string chunk = Encoding.UTF8.GetString(buffer, 0, bytes);
                sb.Append(chunk);
                while (true)
                {
                    string current = sb.ToString();
                    int index = current.IndexOf('\n');
                    if (index == -1) break;

                    string packet = current.Substring(0, index);
                    sb.Remove(0, index + 1);
                    try
                    {
                        try
                        {
                            packetQueue.Enqueue(packet);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError(ex);
                        }
                    }
                    catch(Exception ex)
                    {
                        Debug.LogError("Packet ERROR: " + ex);
                    }
                }
            }
        }
        catch(Exception ex)
        {
            Debug.LogError("Receive ERROR: " + ex);
        }
        Debug.Log("Receive END");
    }
    public async Task Reconnect()
    {
        try
        {
            client?.Close();
        }
        catch { }

        client = new TcpClient();
        await client.ConnectAsync("172.28.5.81", 55555);
        stream = client.GetStream();

        _ = Receive();
    }
    void HandlePacket(string msg)
    {
        Debug.Log("Server: " + msg);
        var packet = msg.Split('|');
        switch (packet[0])
        {
            case "NEW_ROOM":
                {
                    bool exist = false;
                    foreach(var room in roomList)
                    {
                        if (room.roomId == packet[1])
                        {
                            exist = true; break;
                        }
                    }
                    //if (exist)
                    //    break;
                    roomList.Add(new roomInfo
                    {
                        roomId = packet[1],
                        roomName = packet[2],
                        playerCount = Convert.ToInt16(packet[3])
                    });
                    var roomObj = Instantiate(roomPrefab, roomListObj.transform);
                    roomObj.GetComponent<RoomUi>().roomIndex = FindRoomIndex(packet[1]);
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
                    Piece piece = BoardManager.Instance.pieceBoard[from.x, from.y];
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
                        roomId = packet[2];
                    }
                    else if (packet[1] == "COLOR")
                    {
                        if (packet[2] == "WHITE")
                            turn = "WhiteTurnState";
                        else if (packet[2] == "BLACK")
                            turn = "BlackTurnState";
                    }
                    else if (packet[1] == "MATCHED")
                    {
                        gameEnded = false;
                        //StartCoroutine(TurnManager.instance.Matched(packet[2], packet[3]));
                        StartCoroutine(WaitTurnManager(packet));
                    }
                    else if (packet[1] == "ROOM_CREATED")
                    {
                        EnterRoom(packet[2], playerNameInput.text);
                    }
                    break;
                }
            case "TIME":
                {
                    TurnManager.instance.TimeChange(packet[1], Convert.ToInt32(packet[2]));
                    break;
                }
            case "END":
                {
                    if (gameEnded) break;

                    gameEnded = true;

                    if (packet[1] == "WHITE")
                    {
                        TurnManager.instance.GameEnd("White Wins!");
                    }
                    else if (packet[1] == "DRAW")
                    {
                        TurnManager.instance.GameEnd("Draw!");
                    }
                    else
                    {
                        TurnManager.instance.GameEnd("Black Wins!");
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
    IEnumerator WaitTurnManager(string[] packet)
    {
        while(TurnManager.instance == null)
        {
            yield return null;
        }
        StartCoroutine(TurnManager.instance.Matched(packet[2], packet[3]));
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
         Send($"PIECE_MOVE|{roomId}|{from}|{to}");
    }
    public void TurnChange(string turn)
    {
        Send($"TURN_CHANGE|{roomId}|{turn}");
    }
    public void Reload()
    {
        Debug.Log("Reloading");
        CheckConnectionAfterSceneLoad();
        if (client.Connected && stream.CanWrite)
        {
            Send($"LIST_ROOMS");
        }
    }
    public void End(string roomId, string color)
    {
        //if (gameEnded) return;

        //gameEnded = true;

        Send($"END|{roomId}|{color}");
    }
}