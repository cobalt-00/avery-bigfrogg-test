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
        //don't run update if we dont have data
        if (spawnData != null)
        {
            //if we've hit the time to spawn a new cube
            if (currentSpawnTimer < 0)
            {
                //spawn one and get a new time
                SpawnCube();
                currentSpawnTimer = RandomTimeInRange();

            }
            else
            {
                //otherwise, just tick down our timer
                currentSpawnTimer -= Time.deltaTime;
            }
        }
        
    }

    private float RandomTimeInRange()
    {
        //generates a random time within the given intervals
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
