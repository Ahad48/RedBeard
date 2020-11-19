using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Spawns the object at a location, if the object need to be initialsed
/// only once the spawnobject function needs to be called, for multiple spawns at specific time interval
/// just attach the script to the spawn point</para>
/// </summary>

public class SpawnPoint : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float repeatInterval;

    // Start is called before the first frame update
    void Start()
    {
        if (repeatInterval > 0)
        {
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
        }    
    }

    /// <summary>
    /// Spawns the object at the location of the spawn point
    /// </summary>
    /// <returns>Game object that has been instantiated at the location of the spawn point</returns>
    public GameObject SpawnObject()
    {
        if (prefabToSpawn != null)
        {
            return Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
        return null;
    }
}
