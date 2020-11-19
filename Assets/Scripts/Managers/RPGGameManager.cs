using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A singleton class for managing the game
/// </summary>
public class RPGGameManager : MonoBehaviour
{
    // The intance of the virtual camera
    public RPGCameraManager cameraManager;

    // The singleton class check
    public static RPGGameManager sharedInstance = null;

    // The spawn point for the player object of SpawnPoint
    public SpawnPoint playerSpawnPoint;

    // Called just after the object is initialised
    private void Awake()
    {
        if (sharedInstance!=null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }
    private void Start()
    {
        SetupScene();
    }

    /// <summary>
    /// <para>Sets up the scene</para>
    /// </summary>
    void SetupScene()
    {
        SpawnPlayer();
    }

    /// <summary>
    /// <para>Spwans the player at the location</para>
    /// </summary>
    public void SpawnPlayer()
    {
        // Spawning the game object
        GameObject player = playerSpawnPoint.SpawnObject();

        // Set the virtualcamera to follow the player
        cameraManager.virtualCamera.Follow = player.transform;
    }
}
