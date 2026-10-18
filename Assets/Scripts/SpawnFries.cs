using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnFries : MonoBehaviour
{
    public GameObject fryPrefab;
    public GameObject spawnPoint;
    public float sizeVariation = 0.5f;
    public float spawnInterval = 0.05f;
    public float spawnOffset = 0.1f;
    public float numberOfFries = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(InstantiateFries());
        }
    }

    public IEnumerator InstantiateFries()
    {
        // Instantiate fries with random scale and position offset
        for (int i = 0; i < numberOfFries; i++)
        {
            GameObject newFry = Instantiate(fryPrefab, spawnPoint.transform.position, Quaternion.identity);

            // Apply random scale and position offset
            float randomScale = Random.Range(1, 1 + sizeVariation);
            newFry.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            newFry.transform.position += new Vector3(Random.Range(-spawnOffset, spawnOffset), 0, Random.Range(-spawnOffset, spawnOffset));
            newFry.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    
}
