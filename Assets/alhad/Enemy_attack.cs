using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_attack : MonoBehaviour
{
    public List<Transform> li;
    public GameObject enemy_bullet;
    public GameObject enemy_bullet2;
    public Transform enemy_muzzle;
    public GameObject laser;
    GameObject newlaser;

    bool checker = true;
    public Transform enemy_muzzle1;
    public Transform enemy_muzzle2;

    public Transform player;
    public Transform body;
    public float enemyhealth = 100;
    float enemy_health_initial;

    public Animator a;
    float time =0;
    public float firingangle = 30.0f*Mathf.Deg2Rad;

    public Transform laser_muzzle;
    public AudioClip charging;
    public AudioClip lasershoot;
    public bool hurt;

    AudioSource asur;
    private void Start()
    {
        asur = GetComponent<AudioSource>(); 
        enemy_health_initial = enemyhealth;
        body = GameObject.FindGameObjectWithTag("body").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (enemyhealth < enemy_health_initial)
        {
            hurt = true;
            a.SetBool("hurt", hurt);
        }
    }

    void Shoot()
    {
        Rigidbody rb = Instantiate(enemy_bullet2, enemy_muzzle.position, Quaternion.identity).GetComponent<Rigidbody>();
        float d = Vector3.Distance(player.position,rb.position);
        float x = rb.position.y- player.position.y ;
        float alpha,speed;
        Vector3 temp2 = player.position - rb.position;
        Vector3 temp = new Vector3(temp2.x, 0, temp2.z);
        Vector3 axis = Vector3.Cross(temp, temp2);
        if (x > 0)
        {
            alpha = Mathf.Asin(x / d);
            float sqrt = ((-Physics.gravity.y * d * Mathf.Cos(alpha)) / (2 * (Mathf.Tan(alpha) + Mathf.Tan(firingangle))));
            if (sqrt > 0)
                speed = Mathf.Sqrt(sqrt) / Mathf.Cos(firingangle);
            else
                speed = 0;
            Vector3 dir = Quaternion.AngleAxis((alpha + firingangle) * Mathf.Rad2Deg, -axis) * (temp2);
             rb.velocity = dir.normalized * speed;

        }
        else
        {
            x = -x;
            alpha = Mathf.Asin(x / d);
            float sqrt = ((-Physics.gravity.y * d * Mathf.Cos(alpha)) / (2 * (Mathf.Tan(alpha) + Mathf.Tan(firingangle))));
            if (sqrt > 0)
                speed = Mathf.Sqrt(sqrt) / Mathf.Cos(firingangle);
            else
                speed = 0;
            Vector3 dir = Quaternion.AngleAxis((alpha - firingangle) * Mathf.Rad2Deg, -axis) * (temp2);
             rb.velocity = dir.normalized * speed;

        }


    }
    void Shoot2()
    {
        Rigidbody rb = Instantiate(enemy_bullet, enemy_muzzle1.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.velocity = (body.position - rb.position).normalized * 30f;
    }
    void Shoot3()
    {
        Rigidbody rb = Instantiate(enemy_bullet, enemy_muzzle2.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.velocity = (body.position - rb.position).normalized * 30f;
    }
    void Shoot4()
    {
        asur.Stop();
        asur.PlayOneShot(lasershoot);  
        newlaser = Instantiate(laser, laser_muzzle.transform.position, Quaternion.identity);
        // newlaser.transform.parent = laser_muzzle.transform;
         Rigidbody rb = newlaser.GetComponent<Rigidbody>();
         newlaser.GetComponent<VolumetricLines.VolumetricLineBehavior>().isactive =true;
         newlaser.GetComponent<VolumetricLines.VolumetricLineBehavior>().sourcepos = laser_muzzle.transform;
    }
    void startshootlaser()
    {
        asur.PlayOneShot(charging);
    }
   public void laserdestroy()
    {
        Destroy(newlaser);
        asur.Stop();
    }
    public int currenttime()
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            time = 0;
            return 1;
        }
        else
            return 0;
    }
    public void damage(float amount)
    {
        enemyhealth -= amount;
        if (a.GetInteger("action") <= 1)
            a.SetInteger("action", 2);
        // Debug.Log(enemy_health);
        if (checker)
        {
            if (enemyhealth < 0)
            {
                asur.Stop();
                this.GetComponent<enemy_score>().scoreUpdate(this.gameObject);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Spawn_script>().enemy_killed(this.gameObject);
                checker = false;
                player.GetComponent<spawn_enemies>().score_board += 100;
                a.SetTrigger("death");
                a.ResetTrigger("damage");
                Destroy(this.GetComponent<Collider>());
                Destroy(newlaser);
                this.GetComponent<NavMeshAgent>().SetDestination(this.gameObject.transform.position);
                Invoke("destroy", 3f);

                //Instantiate(this.gameObject, transform.position,Quaternion.identity);
                //Destroy(this.gameObject);
            }
            else
            {
                a.SetTrigger("damage");
            }
        }
    }
    public void destroy()
    {
        Destroy(this.gameObject);

    }
}

