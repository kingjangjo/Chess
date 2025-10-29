using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float speed = 5f;
    float rotateSpeed = 7f;
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
            Vector3 rotateValue = new Vector3(-y, x, 0);
            transform.eulerAngles = transform.eulerAngles - rotateValue;
            transform.eulerAngles += rotateValue * rotateSpeed;
        }
    }
}
