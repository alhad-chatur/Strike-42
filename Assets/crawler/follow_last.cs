using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_last : MonoBehaviour
{
    public GameObject Player;

    float time;
    List<Transform> check_points = new List<Transform>();

    //public GameObject crawler;
    public bool nav;
    public Transform goal;
    public UnityEngine.AI.NavMeshAgent agent;
    private ParticleSystem parti;
    private AudioSource audio;

    public float speed;
    private Vector3 direction;
    private Vector3 directionunit;
    public float distance = 50f;
    private Vector3 perpendiculardirection;
    public float randomvalue;
    public Vector3 velocity;
    private Rigidbody rb;
    public float health = 10f;
    private Animator anim;
    public float inrangedistance = 15f;
    public float attackrange = 4f;

    public bool inrange;
    public bool alive;
    public bool attacking;
    public bool attacked;
    public bool rayactive;

    public GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        
        //crawler = GameObject.Find("PA_Warrior Variant 1");
        anim = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        alive = true;
        speed = 4f;
        StartCoroutine(randomvector());
        nav = false;
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        inrange = false;
        anim.SetBool("walk", true);
        parti = GetComponent<ParticleSystem>();
        audio = gameObject.GetComponent<AudioSource>();
        agent.enabled = true;
        Transform obj = GameObject.FindGameObjectWithTag("Active").transform;
        foreach (Transform t in obj)
        {
            check_points.Add(t);
        }
       
        agent.SetDestination(check_points[0].position);

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("die");
            alive = false;
            anim.SetBool("walk", false);
            anim.ResetTrigger("attack0");
            anim.SetBool("attack", false);

            anim.SetTrigger("death");
            //Invoke("death", 1.2f);
        }
        Transform obj = GameObject.FindGameObjectWithTag("Active").transform;
        foreach (Transform t in obj)
        {
            check_points.Add(t);
        }
        if (inrange == false)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.SetDestination(check_points[Random.Range(0, check_points.Count)].position);
            }
            
        }

        Debug.Log(health);

        direction = Player.transform.position - transform.position;
        distance = direction.magnitude;
        
        directionunit = direction.normalized;
        perpendiculardirection = new Vector3(-directionunit.z, 0f, directionunit.x);


        velocity = (directionunit + perpendiculardirection * randomvalue) * speed;

        if (distance < inrangedistance)
        {
            inrange = true;
        }
        if (rayactive && alive)
        {
            RaycastHit ray;

            if (Physics.Raycast(transform.position, Player.transform.position - transform.position, out ray))
            {

                if (ray.collider.tag != "Player" && inrange)
                {
                    Debug.Log("Wall");
                    nav = true;
                    agent.enabled = true;
                }
                else if (inrange && alive && distance > attackrange && !attacking && ray.collider.tag == "Player")
                {
                    follow();
                    Debug.Log("still follow");
                    nav = false;
                    agent.enabled = false;
                }
                //else
                //{
                //  nav = true;
                //agent.enabled = true;

                //}
            }
        }
        if (alive && distance <= attackrange) //attack
        {
            attacking = true;
            Debug.Log("attack");
            anim.SetBool("walk", false);
            anim.SetTrigger("attack 0");
            Quaternion rotation = Quaternion.LookRotation(velocity, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 200 * Time.deltaTime);



        }

        if (nav && !attacking && alive)
        {
            anim.SetBool("walk", true);
            agent.destination = Player.transform.position;
        }


        if (distance < 15)
        {
            inrange = true;

        }
        



    }

    void follow()
    {

        anim.SetBool("walk", true);
        rb.velocity=velocity;
        if (velocity != Vector3.zero && inrange)
        {

            Quaternion rotation = Quaternion.LookRotation(velocity, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 200 * Time.deltaTime);


        }




    }
    public void death()
    {
        Destroy(gameObject);
    }
    IEnumerator attackanimation()
    {

        parti.enableEmission = true;
        yield return new WaitForSecondsRealtime(1.5f);
        parti.enableEmission = false;
        yield return new WaitForSecondsRealtime(1f);
        attacking = false;
        anim.SetBool("walk", true);



    }
    IEnumerator randomvector()
    {
        while (true)
        {
            if (distance > 8)
            {
                randomvalue = Random.Range(-1, 2);
                //Debug.Log("randomvalue is cahnging");
            }
            else
            {
                randomvalue = 0;
            }
            yield return new WaitForSeconds(1f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "bullet" && alive)
        {

            inrange = true;
            health--;
        }


        else if (collision.gameObject.tag != "ground" && alive)
        {



            rayactive = true;



        }

    }
    private void OnParticleCollision(GameObject other)
    {
        if (other == Player)
        {
            Player.GetComponent<health_manager>().player_health -= 0.1f;
        }
    }
    IEnumerator attacksound()
    {
        audio.enabled = true;
        yield return new WaitForSecondsRealtime(1.8f);
        audio.enabled = false;
    }

}
