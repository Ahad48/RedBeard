using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    public RPGCameraManager cameraManager;
    public static RPGGameManager sharedInstance = null;
    public SpawnPoint playerSpawnPoint;

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

    void SetupScene()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject player = playerSpawnPoint.SpawnObject();
        cameraManager.virtualCamera.Follow = player.transform;
    }
}
