using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float speed = 5f;
    float rotateSpeed = 7f;
    private void Start()
    {
        if (ChessClient.Instance.turn == "BlackTurnState")
        {
            this.gameObject.transform.position = new Vector3(0, 10, -8);
            this.gameObject.transform.rotation = Quaternion.Euler(50, 0, 0);
        }
        else if (ChessClient.Instance.turn == "WhiteTurnState")
        {
            this.gameObject.transform.position = new Vector3(0, 10, 8);
            this.gameObject.transform.rotation = Quaternion.Euler(50, 180, 0);
        }

        else
            Debug.LogError("ERROR");

    }
    public void LateUpdate()
    {
        CameraMove();
        CameraRotate();
    }
    void CameraMove()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float verti = Input.GetAxisRaw("Vertical");
        Vector3 pos = new Vector3(hori, 0, verti) * speed * Time.deltaTime;
        this.transform.Translate(pos,Space.Self);
    }
    void CameraRotate()
    {
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            y = Mathf.Clamp(y, -90f, 90f);
            Vector3 rotateValue = new Vector3(-y, x, 0);
            transform.eulerAngles = transform.eulerAngles - rotateValue;
            transform.eulerAngles += rotateValue * rotateSpeed;
        }
    }
}
