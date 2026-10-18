using UnityEngine;

public class ReturnToHome : MonoBehaviour
{

    private Vector3 origPosition;

    private Quaternion origRotation; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // Get the full 3D position vector
        Vector3 currentPosition = transform.position; 
        origPosition = currentPosition;
        Debug.Log("Orig Position: " + currentPosition);

        Quaternion currentRotation = transform.rotation; 
        origRotation = currentRotation;
        Debug.Log("Orig Rotation: " + currentRotation);

        // Access specific axes individually
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "fluzz") 
        {
            Debug.Log("Collision: " + collision.gameObject.name);
            GoHome();
        }
    }

    void GoHome()
    {
        Debug.Log("Going Home... ");
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        transform.position = origPosition;
        transform.rotation = origRotation; 
        gameObject.GetComponent<Rigidbody>().useGravity = true;

    }

    public void GoHomeRotation(GameObject gO)
    {
        Debug.Log("Going to source rotation... " + gO);
        transform.rotation = origRotation; 
    }
}
