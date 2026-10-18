using UnityEngine;
using UnityEngine.InputSystem; 

public class DragObject : MonoBehaviour
{

    private Vector3 mOffset;
    private float mZCoord; 

    private GameObject candidateObject;

    private Quaternion startRotation;

    private float lockedWorldZ; 

    public ReturnToHome returnToHomeScript; 

    private Rigidbody rb;
    private bool isDragging = false;
    private Vector3 physicsTargetPosition;
    private float rotationZDirection = 0f;
    private float rotationXDirection = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnMouseDown()
    {
        // TODO force to spawn point world z 
        lockedWorldZ = gameObject.transform.position.z;
        
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        candidateObject = gameObject; 
        startRotation = gameObject.transform.rotation; 

    }

    void OnMouseDrag()
    {

        rb.useGravity = false; 
        isDragging = true; 

        rb.angularVelocity = Vector3.zero; 

        Vector3 targetPosition  = GetMouseWorldPos() + mOffset; 
        targetPosition.z = lockedWorldZ;

        physicsTargetPosition = targetPosition; 

    }

    
    void OnMouseUp()
    {
        rb.useGravity = true; 
        isDragging = false; 
        candidateObject = null; 

    }

    void Update()
    {
        rotationZDirection = 0f; 
        rotationXDirection = 0f; 
        if (Keyboard.current != null)
        {
            if (Keyboard.current.qKey.isPressed) rotationZDirection = -1f; 
            if (Keyboard.current.eKey.isPressed) rotationZDirection = 1f; 
            if (Keyboard.current.sKey.isPressed) rotationXDirection = 1f;
            if (Keyboard.current.wKey.isPressed) rotationXDirection = -1f;
        }

        if (Mouse.current.rightButton.wasPressedThisFrame && candidateObject != null)
        {
            rb.MoveRotation(startRotation);
        }

    }

    void FixedUpdate()
    {
       if (isDragging)
        {

            Vector3 dragDirection = physicsTargetPosition - rb.position; 
            float maxDistancePerFrame = 0.01f; 

            if (dragDirection.magnitude > maxDistancePerFrame)
            {
                dragDirection = dragDirection.normalized * maxDistancePerFrame; 
            }

            rb.MovePosition(rb.position + dragDirection);
        }

         if ((rotationXDirection != 0f || rotationZDirection != 0f) && candidateObject != null)
        {
            // Calculate a clean physical rotation step
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(rotationXDirection * 2f, 0, rotationZDirection * 2f)); // Adjusted speed factor
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }


    private Vector3 GetMouseWorldPos()
    {
        // pixel x,y co-ords 
        Vector3 mousePoint = Mouse.current.position.ReadValue(); 

        // z co-ord 
        mousePoint.z = mZCoord; 

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

}
