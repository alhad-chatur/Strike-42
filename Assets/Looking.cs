using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looking : MonoBehaviour
{
    private float sense = 200f;
    public Transform body;
    float rot_x = 0f;
    void Start()
    {
      //  body = this.GetComponentInParent<Transform>();
            }

    // Update is called once per frame
    void Update()
    {
        float see_x = Input.GetAxis("Mouse X") * sense * Time.deltaTime;
        float see_y = Input.GetAxis("Mouse Y") * sense * Time.deltaTime;
        rot_x -= see_y;
        rot_x = Mathf.Clamp(rot_x, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rot_x, 0f, 0f);
        body.Rotate(Vector3.up * see_x);


    }
}
