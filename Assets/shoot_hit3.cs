using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using TMPro;

public class shoot_hit3 : MonoBehaviour
{
    // Animator a;
    shaker obj;
    public float damageb = 10;
    private AudioSource audi;
    //public AudioClip ad;
    private GameObject bullets;

    private GameObject k;
    public Transform muzzle1;

    public Vector3 spread2;
    public Transform shoot2;
    public TrailRenderer t;

    private Animator anim;
    int current = 0;
    bool ready = true;
    public RaycastHit shot;

    public TextMeshProUGUI text_gun;
    public TextMeshProUGUI text_ammo;

    public int spread = 100;

    public float timeBwShooting, range, reloadingTime, timeBwShots;
    public int magazineSize, bulletperclick;
    public bool allowButtonHold;
    private int bulletleft, bulletShot;
    private bool singleShot;

    //common bools
    private bool shooting, readyToShoot;

    public bool reloading;

    public Camera fps;
    public Transform point;
    public ParticleSystem flash;
    public ParticleSystem hit_out;
    public ParticleSystem hit_out_terrain;
    public ParticleSystem hit_out_road;
    public ParticleSystem hit_out_wood;
    //public ParticleSystem hit_out_jali;

    public AudioClip rel1;
    public AudioClip rel2;
    public AudioClip rel3;

    //Camera shake
    //public CamShake camShake;
    public float camShakeMagnitude, camShakeDuration;

    private void Awake()
    {
        bulletleft = magazineSize;
        readyToShoot = true;

    }

    void input()
    {

        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
            //Debug.Log(shooting);
            singleShot = false;
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
            //Debug.Log(shooting);
            singleShot = true;
            if (shooting && !reloading)
            {
                audi.PlayOneShot(audi.clip);
                transform.localEulerAngles += new Vector3(-5f, 0, -3f);
                obj.ShakeCamera(camShakeDuration, camShakeMagnitude);
            }
        }

        if ((bulletleft == 0 && !reloading) || (Input.GetKeyDown(KeyCode.R) && !reloading))
        {
            // if (!singleShot) { a.SetBool("shoot2", false); }
            Reload();
        }

        if (readyToShoot && shooting && !reloading && bulletleft > 0)
        {
            bulletShot = bulletperclick;
            flash.Play();
            // if (!singleShot) { a.SetTrigger("shootTrig"); }
            Shoot();
        }

        /* if (ready)
         {
             if (Input.GetMouseButton(0))
             {
                 flash.Play();
                 //transform.localEulerAngles += new Vector3(-0.5f, 0, -0.5f);
                 // a.GetComponent<Animation>().Play();
                 a.SetBool("shoot2",true);
                 Shoot();
             }
             else {
                 //a.StopPlayback();
                // audi.Stop();
                a.SetBool("shoot2",false); 
             }
         }*/
    }

    public void Shoot()
    {

        readyToShoot = false;
        
        Ray middle = fps.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        

        Vector3 target;
        var t2 = Instantiate(t, muzzle1.transform.position, Quaternion.identity);
        t2.AddPosition(muzzle1.transform.position);

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        spread2 = shoot2.position + shoot2.transform.forward * 1000 +new Vector3(x,y,0);
        spread2 += Random.Range(-10, 10) * shoot2.up;
        spread2 += Random.Range(-10, 10) * shoot2.right;
        spread2 -= shoot2.position;
        spread2.Normalize();
        if (Physics.Raycast(shoot2.position, spread2, out shot))
        {
            target = shot.point;
            t2.transform.position = target;
        }
        else
        {
            target = middle.GetPoint(75);
            t2.transform.position = target;
        }


        hit_out.transform.position = target;
        hit_out.transform.forward = shot.normal;

        hit_out_terrain.transform.position = target;
        hit_out_terrain.transform.forward = shot.normal;

        hit_out_road.transform.position = target;
        hit_out_road.transform.forward = shot.normal;

        hit_out_wood.transform.position = target;
        hit_out_wood.transform.forward = shot.normal;

       /* hit_out_jali.transform.position = target;
        hit_out_jali.transform.forward = shot.normal;*/
        //Camera shake
        //StartCoroutine(camShake.Shake(camShakeDuration, camShakeMagnitude));
       // CameraShaker.Instance.ShakeOnce(camShakeMagnitude, 3f, 0.1f, 1f);
        //camShake.Shake(camShakeDuration, camShakeMagnitude);

        if (shot.distance > 0.2f)
        {
            t.GetComponent<Light>().enabled = true;
            t.enabled = true;

            Debug.Log(shot.collider);
            if (shot.collider.CompareTag("Terrain"))
            { Instantiate(hit_out_terrain); }
            else if (shot.collider.CompareTag("Road"))
            { Instantiate(hit_out_road); }
            else if (shot.collider.CompareTag("Wood"))
            { Instantiate(hit_out_wood); }
          /*  else if (shot.collider.CompareTag("Jali"))
            { Instantiate(hit_out_jali); }*/
            else {

                if (!(shot.collider.CompareTag("drone") || shot.collider.CompareTag("drone1") || shot.collider.CompareTag("Respawn") || shot.collider.CompareTag("enemyhead") || shot.collider.CompareTag("Region") || shot.collider.CompareTag("RegionFlying") || shot.collider.CompareTag("bullet_final")))
                {
                    Instantiate(hit_out);
                }


            }
        }
        else
        {

            t.GetComponent<Light>().enabled = false;
            t.enabled = false;

        }
        if (shot.collider)
        {
            if (shot.collider.CompareTag("drone"))
            {
                FindObjectOfType<audiomanager>().Play("metal");
                //Debug.Log("auuu");
                shot.transform.GetComponent<movetoplayer>().hit = true;
                shot.transform.GetComponent<movetoplayer>().enemyhealth -= damageb;
            }
            if (shot.collider.CompareTag("drone1"))
            {
                FindObjectOfType<audiomanager>().Play("metal");
                shot.transform.GetComponent<EnemyMovement>().enemyhealth -= damageb;
                if (shot.transform.GetComponent<EnemyMovement>().enemyhealth > 0)
                {
                    shot.transform.GetComponent<EnemyMovement>().MoveAround();
                }

            }
            if (shot.collider.CompareTag("Respawn"))
            {
                shot.transform.GetComponent<Enemy_attack>().damage(damageb);
                FindObjectOfType<audiomanager>().Play("metal");
            }
            if (shot.collider.CompareTag("crawler"))
            {
                shot.transform.GetComponent<follow_last>().health -= damageb;
                FindObjectOfType<audiomanager>().Play("metal");
            }
        }



        bulletleft--;
        bulletShot--;
        Invoke("ResetShot", timeBwShooting);

        if (bulletShot > 0 && bulletleft > 0)
            Invoke("Shoot", timeBwShots);
        //Invoke("Shoot", tbs);
    }

    public void Reset()
    {
        ready = true;
    }


    void Start()
    {
        // a = this.GetComponent<Animator>();
        audi = this.GetComponent<AudioSource>();
        obj = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<shaker>();
    }


    void Update()
    {
        current = this.GetComponentInParent<switching>().activeWepon;

        if (current == 0)
        {
            text_gun.text = "Current Gun: Berreta";
            text_ammo.text = "Ammo:" + bulletleft + "/" + magazineSize;
        }
        else if (current == 1)
        {
            text_gun.text = "Current Gun: Shotty";
            text_ammo.text = "Ammo:" + bulletleft + "/" + magazineSize;
        }
        else {
            text_gun.text = "Current Gun: Carbine";
            text_ammo.text = "Ammo:" + bulletleft + "/" + magazineSize;
        }
        input();
    }

    private void Reload()
    {
        reloading = true;

        int a = Random.Range(1, 4);
        if (a == 1)
        {
            audi.PlayOneShot(rel1);
        }
        else if (a == 2)
        { audi.PlayOneShot(rel2); }
        else { audi.PlayOneShot(rel3); }




        Invoke("ReloadFinish", reloadingTime);

    }

    private void ReloadFinish()
    {
        bulletleft = magazineSize;
        reloading = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }




}
