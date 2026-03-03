using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        client = new TcpClient();
        await client.ConnectAsync("127.0.0.1", 55555);
        stream = client.GetStream();
        Debug.Log("Server Connected!!");

        _ = Receive(); 

        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnApplicationQuit()
    {
        client?.Close();
    }
    private void Update()
    {
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
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Loby")
        {
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
                        //foreach (var room in roomList)
                        //{
                        //    if (room.roomId == packet[2])
                        //    {
                        //        UIManager.instance.WhiteName.text = packet[3];
                        //        UIManager.instance.BlackName.text = packet[4];
                        //    }
                        //}
                    }
                    else if (packet[1] == "MATCHED")
                    {
                        StartCoroutine(TurnManager.instance.Matched(packet[2], packet[3]));
                    }
                    //매칭 됐다는 로그를 받으면 UIManager에서 닉네임 두개 띄우고 UI하나 띄우고 타이머 시작하기 이때 띄울 UI 만들기
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
                    if (packet[1] == "WHITE")
                    {
                        TurnManager.instance.GameEnd("White Wins!");
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
    public void TurnChange(string turn)
    {
        Send($"TURN_CHANGE|{roomId}|{turn}");
    }
    public void End(string roomId)
    {
        Send($"END|{roomId}");
    }
}