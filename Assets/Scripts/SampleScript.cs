using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class SampleScript : MonoBehaviour
{
    private CinemachineBrain cinemachineBrain;
    void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        CinemachineVirtualCamera activeCamera = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
        if (activeCamera != null)
        {
            Debug.Log("Active Virtual Camera: " + activeCamera.name);
        }
        else
        {
            Debug.LogWarning("Error");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
