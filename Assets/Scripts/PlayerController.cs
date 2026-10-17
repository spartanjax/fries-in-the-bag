using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform head;
    public float moveSpeed = 1f;
    public float maxYaw = 45f;
    public float maxPitch = 45f;

    float yaw;
    float pitch;
    Quaternion startRot;

    void Start()
    {
        startRot = head.localRotation;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(head.position, head.position + head.forward * -2f);
    }

    void Update()
    {
        float curYaw = controlYaw();
        float curPitch = controlPitch();

        head.localRotation = startRot * Quaternion.Euler(curPitch, curYaw, 0f);
    }


    private float controlYaw()
    {
        float input = 0f;
        if (Keyboard.current.aKey.isPressed) input -= 1f;
        if (Keyboard.current.dKey.isPressed) input += 1f;

        yaw += input * 45f * moveSpeed * Time.deltaTime;
        yaw = Mathf.Clamp(yaw, -maxYaw, maxYaw);
        return yaw;
     }

    private float controlPitch()
    {
        float input = 0f;
        if (Keyboard.current.wKey.isPressed) input += 1f;
        if (Keyboard.current.sKey.isPressed) input -= 1f;

        pitch += input * 45f * moveSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);

        return pitch;
    }
}
