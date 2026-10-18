using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonManager : MonoBehaviour
{
    public float buttonCooldown = 1f;
    public GameObject buttonObject;
    [SerializeField] private SpawnFries spawnFriesScript;
    void OnMouseDown()
    {
        Debug.Log(buttonObject.name + " clicked!");

        OnButtonClick();
    }

    public void OnButtonClick()
    {
        spawnFriesScript.StartCoroutine(spawnFriesScript.InstantiateFries());
    }
}
