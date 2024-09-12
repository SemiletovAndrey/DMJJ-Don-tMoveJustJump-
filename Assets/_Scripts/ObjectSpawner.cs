using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int spawnCount = 10;
    public Vector3 spawnInterval;

    public void SpawnObjects()
    {
        if (prefabToSpawn == null)
        {
            Debug.LogWarning("Prefab not assigned!");
            return;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(i * spawnInterval.x, 0, i * spawnInterval.z);
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, transform);
            spawnedObject.name = prefabToSpawn.name + "_" + i;
        }
    }
}
