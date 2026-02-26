using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PromotionManager : MonoBehaviour
{
    public static PromotionManager instance;
    public GameObject whitePromotionPanel; // 프로모션 패널
    public GameObject blackPromotionPanel; // 프로모션 패널
    private GameObject promotedPiece;
    private bool white;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PushButton(GameObject promotionPiece)
    {
        Vector3 position = promotedPiece.transform.position;
        Vector2Int piecePosition = promotedPiece.GetComponent<Piece>().Pos;
        bool white = promotedPiece.GetComponent<Piece>().white;
        Destroy(promotedPiece);
        GameObject newPiece = Instantiate(promotionPiece, position, quaternion.identity);
        newPiece.GetComponent<Piece>().X = piecePosition.x;
        newPiece.GetComponent<Piece>().Y = piecePosition.y;
        if(white)
            whitePromotionPanel.SetActive(false);
        else
            blackPromotionPanel.SetActive(false);
        TurnManager.instance.ChangeTurn();
    }
    public void Promote(GameObject piece)
    {
        promotedPiece = piece;
        white = piece.GetComponent<Piece>().white;
        if (white)
            whitePromotionPanel.SetActive(true);
        else
            blackPromotionPanel.SetActive(true);
    }
}
