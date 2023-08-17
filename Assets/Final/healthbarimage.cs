using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbarimage : MonoBehaviour
{
    private Image agege;
    private float CurrentHealth;
    public float MaxHealth = 1000f;
    health_manager player;
    // Start is called before the first frame update
    void Start()
    {
        agege = GetComponent<Image>();
        player = FindObjectOfType<health_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth = player.player_health;
        agege.fillAmount = CurrentHealth / MaxHealth;

        
    }
}
