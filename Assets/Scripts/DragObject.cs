using UnityEngine;
using UnityEngine.InputSystem; 

public class DragObject : MonoBehaviour
{

    private Vector3 mOffset;
    private float mZCoord; 

    private GameObject candidateObject;

    private Quaternion startRotation;

    private bool b_IsRotating = false; 
    private bool isRotatingLeft = false; 
    private bool isRotatingRight = false; 

    private float lockedWorldZ; 

    public ReturnToHome returnToHomeScript; 


    void OnMouseDown()
    {
        // TODO force to spawn point world z 
        lockedWorldZ = gameObject.transform.position.z;
        
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        candidateObject = gameObject; 
        startRotation = gameObject.transform.rotation; 

    }

    void Update()
    {
        b_IsRotating = false; 
        isRotatingLeft = false;
        isRotatingRight = false;

        if (Keyboard.current != null && Keyboard.current.qKey.isPressed)
        {
            b_IsRotating = true;
            isRotatingLeft = true;
            isRotatingRight = false;
            Debug.Log("Q pressed, candidate = " + candidateObject);
        }

        if (Keyboard.current != null && Keyboard.current.eKey.isPressed)
        {
            b_IsRotating = true;
            isRotatingRight = true;
            isRotatingLeft = false;
            Debug.Log("E pressed, candidate = " + candidateObject);
        }

        if (b_IsRotating && candidateObject != null)
        {
            if (isRotatingLeft) { candidateObject.transform.Rotate(0,0,-1f); }
            if (isRotatingRight) { candidateObject.transform.Rotate(0,0,1f); }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame && candidateObject != null)

            // TODO this will return the rotation correctly but it sucks. 
            //returnToHomeScript = GetComponent<ReturnToHome>();
            //returnToHomeScript.GoHomeRotation(candidateObject);
            candidateObject.transform.rotation = startRotation;
            b_IsRotating = false; 
            isRotatingLeft = false;
            isRotatingRight = false;
    }

    void OnMouseUp()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        candidateObject = null; 

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

        Vector3 targetPosition  = GetMouseWorldPos() + mOffset; 
        targetPosition.z = lockedWorldZ;

        transform.position = targetPosition; 
        candidateObject = gameObject;

    }
}
