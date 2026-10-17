using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    public GameObject camera;
    public float maxXRot = 45;
    public float maxZRot = 45;

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.isPressed && camera.transform.rotation.x < maxXRot)
        {
            camera.transform.Rotate(new Vector3 (-45, 0, 0) * Time.deltaTime);
        }
        if (Keyboard.current.sKey.isPressed && camera.transform.rotation.x > -maxXRot)
        {
            camera.transform.Rotate(new Vector3 (45, 0, 0) * Time.deltaTime);
        }

        if (Keyboard.current.aKey.isPressed && camera.transform.rotation.z < maxZRot)
        {
            camera.transform.Rotate(new Vector3 (0, -45, 0) * Time.deltaTime);
        }
        if (Keyboard.current.dKey.isPressed && camera.transform.rotation.z > -maxZRot)
        {
            camera.transform.Rotate(new Vector3 (0, 45, 0) * Time.deltaTime);
        }
    }
}
