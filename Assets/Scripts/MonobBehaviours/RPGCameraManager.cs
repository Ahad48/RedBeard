using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// A singleton class
/// Finds the virtual camera
/// </summary>
public class RPGCameraManager : MonoBehaviour
{
    public static RPGCameraManager sharedInstance = null;

    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        // If another instance of the class is found then detroy this game object
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }
        GameObject vCamObject = GameObject.FindWithTag("VirtualCamera");

        // Get the virtual camera
        virtualCamera = vCamObject.GetComponent<CinemachineVirtualCamera>();
    }
}
