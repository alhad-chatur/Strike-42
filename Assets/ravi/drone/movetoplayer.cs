using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;
public class movetoplayer : MonoBehaviour
{
    Animation a;
    static float t = 0.0f;
    public float knockbackTime = 1;
    public float kick = 1.4f;
    private Transform goal;
    private NavMeshAgent agent;
    public bool hit;
    private ContactPoint contact;
    private float timer;
    public float enemyhealth = 120f;
    public GameObject enemy;
    private float movementx;
    private float movementy;
    private float movementz;
    bool scoreNotAdded = true;
    //private string runanimationhai = "run";
    private string hitanimationhai = "hit";
   // private string kyayaranimation = "kyayar";
    Animator anim;
    public Transform enemy_muzzle1;
    public Transform enemy_muzzle2;
    public GameObject enemy_bullet;
    public Transform enemy_muzzle;
    Transform player;
    shoot_hit2 ravi;
   // AudioSource asur;
   // public AudioClip a1;
   // public AudioClip a2;
    shoot_hit3 ravi1;
    shoot_hit shirsat;
    bool isattacking=false;

    //public RaycastHit shot1;
  //  RaycastHit shot1;
    // public Animation anim2;
  //

    // public int killedenemies;
    //private string deadanimation = "dead";
    //Vector3 movement;
    //private string attackanimation = "attack";
    

    void Start()
    {
       // a = this.GetComponentInParent<Animation>();
       // a["blades"].layer = 123;
        goal = GameObject.FindGameObjectWithTag("Player").transform;
        ravi = goal.gameObject.GetComponentInChildren<shoot_hit2>();
        shirsat = goal.gameObject.GetComponentInChildren<shoot_hit>();
        ravi1 = goal.gameObject.GetComponentInChildren<shoot_hit3>();

       // Debug.Log(ravi.name);
        agent = GetComponent<NavMeshAgent>();
        //Set timer to the same a knockback in first instance.
        timer = knockbackTime;
        anim = GetComponent<Animator>();
       player = GameObject.FindGameObjectWithTag("Player").transform;
        //shot1 = GameObject.FindGameObjectWithTag("gun").GetComponent<shoot_hit2>().shot;
        //  shoot_hit2 sn = gameObject.GetComponent<shoot_hit2>();
        // anim2 = GetComponent<Animation>();
        // shotting2= FindObjectOfType<shoot_hit2>();

        StartCoroutine("shooting");

    }
    void Update()
    {   

        //Debug.Log(ravi.name);
        // Debug.Log(shot1);
        /*if (shot1.collider.CompareTag("drone"))
          {
              FindObjectOfType<audiomanager>().Play("bullethit2");
              enemyhealth -= 10;
              //contact = collision.conta;
              hit = true;
              anim.SetBool(hitanimationhai, true);
              //Debug.Log("ravi!!!!");

          }*/
        //  anim.SetBool(kyayaranimation, true);
        //anim2.Play("blademove");
        // a.Play("blades");
        // a.Play("blades1");
        // a.Play();
        // a.Play("blades");
        
        float y2 = player.position.y;
        float y1 = enemy.transform.position.y;
        
        float x2 = player.position.x;
        float x1 = enemy.transform.position.x;
        //Debug.Log(x2);
        float z2 = player.position.z;
        float z1 = enemy.transform.position.z;  
        float newY = y2 - y1;
        float newX = x2 - x1;
        float newZ = z2 - z1;   

        float Y = newY * newY;
        float X = newX * newX;
        float Z = newZ * newZ;  

        float Answer = Y + X + Z;

        float xRot = 0;
        
        

        float Distance = Mathf.Sqrt(Answer);
        //Debug.Log(Distance);
        if(Distance < 20 && enemyhealth>0 )
        {
            isattacking = true;
            //anim.SetBool(hitanimationhai, true);
            //ransform.rotation = Quaternion.Lerp(transform.rotation,new Quaternion(30,0,0,0),2*Time.deltaTime);
            
            xRot = Mathf.Lerp(0, 30, t);
            this.transform.eulerAngles = new Vector3(xRot, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
            if(t < 1)
            {
                t += 2f * Time.deltaTime;
            }    
      
            //this.transform.eulerAngles = Vector3.Slerp(new Vector3(0, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z), new Vector3(30, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z), 100* Time.deltaTime);
        }
        else if(Distance > 18  )
        {
            // anim.SetBool(hitanimationhai, false);
            isattacking = false;
                xRot = Mathf.Lerp(0, 30, t);
                this.transform.eulerAngles = new Vector3(xRot, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
                if (t > 0)
                {
                    t -= 2f * Time.deltaTime;
                }
     
            //transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(00, 0, 0, 0), 2 * Time.deltaTime);
            //this.transform.eulerAngles = Vector3.Slerp(new Vector3(30,this.transform.localEulerAngles.y, this.transform.localEulerAngles.z), new Vector3(0, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z), 100 * Time.deltaTime);
            // Debug.Log("ravu shitsay");
        }

       
        agent.SetDestination(goal.position);
        //runanimation();
        //
      
        if (enemyhealth <= 0)
        {
            anim.SetBool(hitanimationhai, true);
            if (scoreNotAdded)
            {
               // Debug.Log("haoo");
                this.GetComponent<enemy_score>().scoreUpdate(this.gameObject);
               // Debug.Log("haoo2");
                scoreNotAdded = false;
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<Spawn_script>().enemy_killed(this.gameObject);
            // Debug.Log(enemyhealth);
            //Destroy(this.gameObject);
            Invoke("finish",1.7f);

            //is.transform.eulerAngles = Vector3.Slerp(this.transform.eulerAngles, new Vector3(0, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z), 2 * Time.deltaTime);
            // killedenemies = killedenemies + 1;  

        }


      //this.transform.eulerAngles = Vector3.Slerp(new Vector3(0, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z), new Vector3(30, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z), 2 * Time.deltaTime);
        if (hit && enemyhealth>0)
          {     
              //Allow physics to be applied.
               gameObject.GetComponent<Rigidbody>().isKinematic =false;
              //Stop our AI navigation.
              gameObject.GetComponent<NavMeshAgent>().isStopped = true;

            //Push back our enemy with an impulse force set via the kick value.
            // Debug.Log("yoyoyo..");

            if (GameObject.FindGameObjectWithTag("Player").GetComponent<switching>().activeWepon == 0)
            {
                ravi = goal.gameObject.GetComponentInChildren<shoot_hit2>();
                gameObject.GetComponent<Rigidbody>().AddForceAtPosition(Camera.main.transform.forward * kick, ravi.shot.point, ForceMode.Impulse);

            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<switching>().activeWepon == 1)
            {
                ravi1 = goal.gameObject.GetComponentInChildren<shoot_hit3>();
                gameObject.GetComponent<Rigidbody>().AddForceAtPosition(Camera.main.transform.forward * kick, ravi1.shot.point, ForceMode.Impulse);
            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<switching>().activeWepon == 2)
            {
                shirsat = goal.gameObject.GetComponentInChildren<shoot_hit>();
                gameObject.GetComponent<Rigidbody>().AddForceAtPosition(Camera.main.transform.forward * kick, shirsat.shot.point, ForceMode.Impulse);
            }
            hit = false;
          // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), rotationSpeed * Time.fixedDeltaTime);
            timer = 0;
          }
          else
          {
              timer += Time.deltaTime;
              //After being knocked back,restart movement after X seconds.
          if (knockbackTime < timer)
              {
                  gameObject.
                 GetComponent<Rigidbody>().isKinematic =
                 true;
                  gameObject.
                 GetComponent<NavMeshAgent>().isStopped =
                 false;
                  agent.
                 SetDestination(goal.position);
              }
          }
    }
    //  void OnCollisionEnter(Collision collision)
    //{
    /* if (collision.transform.CompareTag("Player"))
     {
         anim.SetBool(attackanimation, true);
     }
     else
     {
         anim.SetBool(attackanimation, false);   
     }*/
    //We compare the tag in the othe object to the tag name we set earlier.

    /* else if(collision.transform.CompareTag("floor"))
    {
        //Debug.Log("ravi floor ");
        anim.SetBool(hitanimationhai, false);
    }
    }
    else
    {
        //Debug.Log("ravi1");
        anim.SetBool(hitanimationhai, false);
    }*/
    //}
  
    IEnumerator shooting()
    {
        while(true)
        {
            if (isattacking == true)
            {
                yield return new WaitForSeconds(0.5f);
                Rigidbody rb = Instantiate(enemy_bullet, enemy_muzzle.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.velocity = (player.position - rb.position).normalized * 30f;
            }
            else
                yield return null;
        }

    }

    
    void finish()
    {
        Destroy(this.gameObject);
    }


}