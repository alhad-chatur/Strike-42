using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Destroy2 : MonoBehaviour
{
    private float timeLeft;
    public void Awake()
    {
        ParticleSystem system = GetComponent<ParticleSystem>();
        timeLeft = system.startLifetime;
    }
    public void Update()
    {
        timeLeft -= Time.deltaTime * 0.8f;
        if (timeLeft <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}