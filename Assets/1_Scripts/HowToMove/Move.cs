using UnityEngine;

public class Move : MonoBehaviour
{
    private Transform piecePosition;
    private void Start()
    {
        piecePosition = transform.parent;
    }
    private void OnMouseDown()
    {
        Debug.Log("Click");
        piecePosition.position = new Vector3(this.gameObject.transform.position.x,0,this.transform.position.y);
    }
}
