using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet : MonoBehaviour
{
    Rigidbody rb;
    public GameObject explosion;
    [SerializeField]
    AudioSource asource;

    public float damage2;
   
    public void destroy()
    {
        Destroy(gameObject);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        transform.forward = rb.velocity;
       // asource = gameObject.GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
            }
            asource.Play();
            collision.gameObject.GetComponentInParent<health_manager>().damage(damage2);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<TrailRenderer>().enabled = false;
            GetComponent<Light>().enabled = false;
        }
        Invoke("destroy", 1.0f);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
    }
}
