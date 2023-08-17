using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Restore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hr"))
        {
            gameObject.GetComponent<health_manager>().player_health = gameObject.GetComponent<health_manager>().max_health;
        }
        Destroy(other.gameObject);
    }
}
