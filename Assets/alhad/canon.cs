using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canon : MonoBehaviour
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

    int collisions;

    PhysicMaterial pm;

    bool isexplosion = false;

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
        isexplosion = true;
         if(explosion!=null)
         {
             Instantiate(explosion, transform.position, Quaternion.identity);
         }
        
        enemy = Physics.OverlapSphere(transform.position,range,enemies);
        //Debug.Log(enemies.value);
           // Debug.Log("Player Detected");
            for (int i = 0; i < enemy.Length; i++)
            {
               enemy[i].GetComponentInParent<health_manager>().damage(damage2);
            }
        this.GetComponent<AudioSource>().Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Light>().enabled = false;

        Invoke("destroy", 1.5f);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        material();
    }

    // Update is called once per frame
    void Update()
    {
        max_l -= Time.deltaTime;
        if(max_l<=0 && isexplosion==false)
        {
            Explode();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        collisions++;

        if(collision.collider.CompareTag("Player"))
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
