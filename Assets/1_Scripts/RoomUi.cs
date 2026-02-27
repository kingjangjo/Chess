using UnityEngine;
using TMPro;
public class RoomUi : MonoBehaviour
{
    public TextMeshProUGUI roomNameText;
    public TextMeshProUGUI roomIdText;
    public TextMeshProUGUI roomPlayerCountText;
    public int roomIndex;
    private void Update()
    {
        if (ChessClient.Instance.client.Connected)
        {
            roomNameText.text = ChessClient.Instance.roomList[roomIndex].roomName;
            roomIdText.text = "ID:"+ChessClient.Instance.roomList[roomIndex].roomId;
            roomPlayerCountText.text = ChessClient.Instance.roomList[roomIndex].playerCount.ToString()+"/2";
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PushEnterRoomButton()
    {
        string playerName = ChessClient.Instance.playerNameInput.text;
        ChessClient.Instance.EnterRoom(ChessClient.Instance.roomList[roomIndex].roomId, playerName);
    }
}
