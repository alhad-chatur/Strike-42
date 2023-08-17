using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_attack2 : MonoBehaviour
{
    public GameObject enemy_bullet;
    public Transform enemy_muzzle1;
    public Transform enemy_muzzle2;
    public Transform player;
    private float enemy_health = 100;
    public Animator a;
    void Shoot2()
    {
        Rigidbody rb = Instantiate(enemy_bullet, enemy_muzzle1.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.velocity = (player.position - rb.position).normalized * 30f;
    }
    void Shoot3()
    {
        Rigidbody rb = Instantiate(enemy_bullet, enemy_muzzle2.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.velocity = (player.position - rb.position).normalized * 30f;
    }
    public void damage(float amount)
    {
        enemy_health -= amount;
        if (enemy_health < 0)
        {
            a.SetTrigger("death");
            Destroy(this.GetComponent<Collider>());
            //Destroy(this.gameObject);
        }
    }
}

