using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Scriptable Objects/SpawnData")]
public class SpawnData : ScriptableObject
{
    public float minSpawnInterval;
    public float maxSpawnInterval;

    public float spawnBoundsMin;
    public float spawnBoundsMax;

    public float spawnHeight;

}
