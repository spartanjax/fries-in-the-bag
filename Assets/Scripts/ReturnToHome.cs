using UnityEngine;

public class ReturnToHome : MonoBehaviour
{

    private float firstXPos;
    private float firstYPos;
    private float firstZPos; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // Get the full 3D position vector
        Vector3 currentPosition = transform.position; 
        Debug.Log("Current Position: " + currentPosition);

        // Access specific axes individually
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        float yPos = transform.position.y;
        if (yPos <= 0)
        {
            Debug.Log("Go Home Command Sent: " + currentPosition);
            GoHome(); 
        }
    }

    void GoHome()
    {
        Debug.Log("Going Home: " + new Vector3(firstXPos, firstYPos, firstZPos));
        transform.position = new Vector3(firstXPos, firstYPos, firstZPos);
    }
}
