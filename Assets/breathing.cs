using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breathing : MonoBehaviour
{
    public Transform weapon;
    public Vector3 origin;

    private Vector3 lerping;
    private float idle;
    private float moving;
    private float mag;
    void Start()
    {
        origin = weapon.localPosition;
    }

    void Update()
    {
        if (Input.GetMouseButton(1)==false)
        {
            
            mag = GameObject.Find("Cylinder").GetComponent<Movement>().move.magnitude;
            if (mag < 0.1)
            {
                breathe(idle, 0.035f, 0.035f);
                idle += Time.deltaTime * 1.5f;
                weapon.localPosition = Vector3.Lerp(weapon.localPosition, lerping, Time.deltaTime * 2f);
            }
            else
            {
                breathe(moving, 0.045f, 0.045f);
                moving += Time.deltaTime * 3f;
                weapon.localPosition = Vector3.Lerp(weapon.localPosition, lerping, Time.deltaTime * 3f);
            }
        }

    }

    void breathe(float z, float intensity_x,float intensity_y)
    {
        lerping = origin+ new Vector3(Mathf.Cos(z)*intensity_x, Mathf.Sin(z*2)*intensity_y,0);
    }
}
