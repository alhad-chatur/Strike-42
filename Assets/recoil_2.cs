using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recoil_2 : MonoBehaviour
{

    // public Vector3 rec_up;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && this.GetComponent<shoot_hit3>().reloading != true)
        {
            transform.localEulerAngles += new Vector3(-5f, 0, -3f);
        }

    }












}