using UnityEngine;
using UnityEngine.Rendering;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject BlueBox;
    [SerializeField] private GameObject RedBox;

    [SerializeField] private SpawnData spawnData;

    private float currentSpawnTimer = -1f;

    private void Start()
    {
        if (spawnData == null)
        {
            Debug.LogError("No spawn data given! Game will not function.");
        } else
        {
            currentSpawnTimer = RandomTimeInRange();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnData != null)
        {
            if (currentSpawnTimer < 0)
            {
                SpawnCube();
                currentSpawnTimer = RandomTimeInRange();

            }
            else
            {
                currentSpawnTimer -= Time.deltaTime;
            }
        }
        
    }

    private float RandomTimeInRange()
    {
        return Random.Range(spawnData.minSpawnInterval, spawnData.maxSpawnInterval);
    }

    private void SpawnCube()
    {
        //get a random box
        int isRed = Random.Range(0, 2);
        GameObject prefab = isRed == 1 ? RedBox : BlueBox;

        //give it a randomized position
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnData.spawnBoundsMin, spawnData.spawnBoundsMax),
            spawnData.spawnHeight,
            0);
            
        //spawn our box at the random position with a neutral rotation
        Instantiate(prefab, spawnPosition, Quaternion.identity);
        
    }
}
