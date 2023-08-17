using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sway : MonoBehaviour
{

    public float intensity;
    public float smooth;

    private Quaternion rotation;
    // Update is called once per frame

    private void Start()
    {
        rotation = transform.localRotation;
    }
    void Update()
    {
        Swaying();
    }

    void Swaying()
    {
        float x_sway = Input.GetAxis("Mouse X");
        float y_sway = Input.GetAxis("Mouse Y");

        Quaternion rotation2 = Quaternion.AngleAxis(-intensity*x_sway, Vector3.up);
        Quaternion rotation3 = Quaternion.AngleAxis(intensity * y_sway, Vector3.right);

        Quaternion final_rot = rotation * rotation2*rotation3;
        transform.localRotation = Quaternion.Lerp(transform.localRotation,final_rot,Time.deltaTime*smooth);


    }
}
