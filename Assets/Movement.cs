using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public CharacterController cc;
    public float h = 3f;
    public float move_speed = 15f;
    public float g = -9.81f;
    public Vector3 move;
    Vector3 vel;
    void Update()

    {

        float move_x = Input.GetAxis("Horizontal");
        float move_z = Input.GetAxis("Vertical");
        move = transform.right * move_x + transform.forward * move_z;
        cc.Move(move * move_speed * Time.deltaTime);

        vel.y += g * Time.deltaTime;
        cc.Move(vel * Time.deltaTime);
        if(cc.isGrounded)
        {
            vel.y = -1f;   
        }
        if(Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            vel.y = Mathf.Sqrt(h * -1 * g);
        }
        
    }
}
