using UnityEngine;
using UnityEngine.InputSystem; 

public class DragObject : MonoBehaviour
{

    private Vector3 mOffset;
    private float mZCoord; 

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;

    }

    private Vector3 GetMouseWorldPos()
    {
        // pixel x,y co-ords 
        Vector3 mousePoint = Mouse.current.position.ReadValue(); 

        // z co-ord 
        mousePoint.z = mZCoord; 

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseDrag()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        transform.position = GetMouseWorldPos() + mOffset; 
    }
}
