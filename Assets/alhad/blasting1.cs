using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blasting1 : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask enemies;

    [Range(0,1)]
    public float bounciness;

    public float damage2;
    public Collider[] enemy;
    public float range;

    public int max_c;
    public float max_l;
    public bool touch=true;
    AudioSource asur;

    int collisions;

    PhysicMaterial pm;

    void material()
    {
        pm = new PhysicMaterial();
        pm.bounciness = bounciness;
        pm.frictionCombine = PhysicMaterialCombine.Minimum;
        pm.bounceCombine = PhysicMaterialCombine.Maximum;
        GetComponent<SphereCollider>().material = pm;
    }

   

    private void Explode()
    {
        
         if(explosion!=null)
         {
             Instantiate(explosion, transform.position, Quaternion.identity);
         }
        
        enemy = Physics.OverlapSphere(transform.position,range,enemies);
        //Debug.Log(enemies.value);
        if (enemies.value.Equals(131072))
        {
           // Debug.Log("Player Detected");
            for (int i = 0; i < enemy.Length; i++)
            {
               enemy[i].GetComponentInParent<health_manager>().damage(damage2);
            }
        }
        else
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                //enemy[i].GetComponent<Enemy_attack>().damage(damage2);
            }
        }
        if (asur.enabled == false)
        {
            asur.enabled = true;
            asur.Play();
        }
        Invoke("destroy", 1.0f);

    }

    public void destroy()
    {
        Destroy(gameObject);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        material();
        asur = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if(collisions>max_c)
        {
            Explode();
        }
        max_l -= Time.deltaTime;
        if(max_l<=0)
        {
            Explode();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        collisions++;

        if(collision.collider.CompareTag("Respawn") && touch)
        {
            Explode();
        }
        if(collision.collider.CompareTag("Untagged") && touch )
        {
            Explode();
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
