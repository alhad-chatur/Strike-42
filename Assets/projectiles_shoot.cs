using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectiles_shoot : MonoBehaviour
{
   
    private GameObject bullets;
    public GameObject a;
    public GameObject a2;
    public GameObject a3;
    private GameObject k;


    public float f_shoot, f_up;

    public float time_shooting, spread, t_shots;

    //public int magazine_size, bulletspertap;

    bool shooting, ready;
    bool invoke = true;


    public Camera fps;
    public Transform point;
    public ParticleSystem flash;
    private void Awake()
    {
        ready = true;
        k = a;
    }

    void input(GameObject x11)
    {
        if (ready)
        {
            if (Input.GetMouseButton(0))
            {
                flash.Play();
                Shoot(x11);
            }
        }
    }

    void Shoot(GameObject y22)
    {



        Ray middle = fps.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit shot;
        Vector3 target;

        if(Physics.Raycast(middle,out shot))
        {
            target = shot.point;
        }
        else
        {
            target = middle.GetPoint(75);
        }
        Vector3 dir = target - point.position;
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);


        Vector3 dir2 = dir + new Vector3(x, y, 0);

       
       
        bullets = Instantiate(y22, point.position, Quaternion.identity);
       

        

        bullets.transform.forward = dir2.normalized;

        bullets.GetComponent<Rigidbody>().AddForce(dir2.normalized * f_shoot, ForceMode.Impulse);
        bullets.GetComponent<Rigidbody>().AddForce(fps.transform.up * f_up, ForceMode.Impulse);


        if(invoke)
        {
            Invoke("Reset", time_shooting);
            invoke = false;
        }




    }

    public void Reset()
    {
        ready = true;
        invoke = true;
    }


    void Start()
    {
        
    }

   
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            k = a;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            k = a2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            k = a3;
        }*/
        k = a2;

        input(k);
        
        
    }
}
