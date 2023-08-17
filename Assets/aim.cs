using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim : MonoBehaviour
{
    public Transform gun;
    public Transform sharp_aim;
    public Transform original;
    private bool switching;
  //  int count = 0;
    public AudioClip aim_sound;
    public AudioSource audio;
   // private float mag;
    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        switching = GetComponentInParent<switching>().switchin;
        // mag = GameObject.Find("Cylinder").GetComponent<Movement>().move.magnitude;


        aiming(Input.GetMouseButton(1));
        if (Input.GetMouseButtonDown(1))
        {
            audio.PlayOneShot(aim_sound);        }
        
    }

    void aiming(bool a)
    {
        if(a)
        {
            gun.position = Vector3.Lerp(gun.position, sharp_aim.position, Time.deltaTime*20);
        }
        else if (!switching)
        {
            gun.position = Vector3.Lerp(gun.position, original.position, Time.deltaTime *20);
        }
    }
}
