using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class UITransitionManager : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;
 


    // Start is called before the first frame update
    public void Start()
    {
        currentCamera.Priority++;
    }

    // Update is called once per frame
    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        currentCamera.Priority--;
        currentCamera = target;
        currentCamera.Priority++;

    }
}
