using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;


public class EnemyMovement : MonoBehaviour
{
    Transform Player;
   
    Rigidbody rb;
   public AudioSource audi;
   public AudioClip lasersound;
    public AudioClip backsound;
    public float rotationSpeed;
    public float moveSpeed;
    public float enemyhealth = 120f;
    float max_health;
    public float maxSpeed;
    public float minRange;
    public float maxRange;
    public float moveRange;
    CharacterController controller;
    public Transform enemy;
    public Transform enemy_muzzle;
    public GameObject enemy_bullet;
    public GameObject laser;
    GameObject newlaser;
    public Transform laser_muzzle;
    bool isattacking = false;
   private string deathanimation = "death";
    
    private string hitanimationhaibhai = "hit";
   
    public ParticleSystem flash;
    bool scoreNotAdded = true;

    Animator ainm;
   
    public ParticleSystem system;

  

    void Start()
    {
        max_health = enemyhealth;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        controller = GetComponent<CharacterController>();
       ainm = GetComponent<Animator>();
       audi = this.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(coroutineA());
      
    }
    void Update()
    {
 
        if (enemyhealth <= 0)
        {
            if (scoreNotAdded)
            {
                this.GetComponent<enemy_score>().scoreUpdate(this.gameObject);
                scoreNotAdded = false;
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<Spawn_script>().enemy_killed(this.gameObject);
            StopCoroutine(coroutineA());
            Die();
            ainm.SetBool(deathanimation, true);
           
           
        }
        deamspeedrunsound();
        
        FollowPlayer();
                
      
       // Debug.Log(enemyhealth);
        
       
  
    }
  
    public void Die()

    {
        Destroy(newlaser);
        system.Play();
       // Instantiate(system);
        StartCoroutine(blast());
       
       // Destroy(dea, 2);
    }
    IEnumerator blast()
    {
        
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);

    }
    IEnumerator coroutineA()
    {
        while (true)
        { 
           if (enemyhealth <= 0)
            {
                yield return null;

            }
           
            movearound3();
            yield return new WaitForSeconds(3.0f);

        }
    }
     public void FollowPlayer()
    {
      

            Vector3 playerPos = Player.position;
            Vector3 lookDir = playerPos - transform.position;
            Vector3 moveDir = lookDir;// * moveSpeed;
            moveDir *= Time.fixedDeltaTime;


            if (((Vector3.Distance(transform.position, playerPos) <= maxRange) && (Vector3.Distance(transform.position, playerPos) > minRange)) || (enemyhealth<max_health) && (Vector3.Distance(transform.position, playerPos) > minRange))
            {
               
                Vector3 previous = transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), rotationSpeed * Time.fixedDeltaTime);
                controller.Move(moveDir);
                float velocity = ((transform.position - previous).magnitude) / Time.fixedDeltaTime;

                previous = transform.position;

            }
        if (Vector3.Distance(transform.position, playerPos) < minRange)
        {
            
            Shoot1();
            

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), rotationSpeed * Time.fixedDeltaTime);
            controller.Move(Vector3.zero);
        }
        else
        {
            isattacking = false;
            if (newlaser!=null)
            {
                Destroy(newlaser);
            }
            if(audi.clip!=backsound)
            {
                audi.clip = backsound;
                audi.Play();
            }
        }
    }
    public void MoveAround()
    {
       

            Vector3 playerPos = Player.position;
            Vector3 randomPos = Random.insideUnitSphere * moveRange;


            
                Vector3 moveDir = randomPos;
                moveDir *= Time.fixedDeltaTime;

                controller.Move(moveDir);




            
            
        
    }
   
    
     public void movearound3()
    {
       
        Vector3 playerPos = Player.position;

        if (Vector3.Distance(transform.position, playerPos) > maxRange)
        {
           float x1 = Random.Range(1f, 100f);
            float    z3 = Random.Range(1f, 100f);
         
           Vector3 randomPos =  new Vector3(x1, 0 ,z3 );
            randomPos=randomPos.normalized;
            Vector3 moveDir = randomPos* 5f;
        

            Vector3 lookDir = Player.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), rotationSpeed * Time.fixedDeltaTime);
            transform.Translate(randomPos);
  
           
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxRange);
        Gizmos.DrawWireSphere(transform.position, minRange);
    }
    void deamspeedrunsound()
    {
       
    }
    void Shoot1()
    {
        if (isattacking == false)
        {
            isattacking = true;
            newlaser = Instantiate(laser, laser_muzzle.transform.position, Quaternion.identity);
            newlaser.transform.parent = laser_muzzle.transform;
            Rigidbody rb = newlaser.GetComponent<Rigidbody>();
        }
        newlaser.GetComponent<VolumetricLines.VolumetricLineBehavior>().isactive = true;
        newlaser.GetComponent<VolumetricLines.VolumetricLineBehavior>().sourcepos = laser_muzzle.transform;
        if(audi.clip !=lasersound)
        {
            audi.clip = lasersound;
            audi.Play();
        }
       
    }
}