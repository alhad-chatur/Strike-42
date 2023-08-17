using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class health_manager : MonoBehaviour
{
   public float max_health = 1000;
    public float player_health;
    private bool go;
    public TextMeshProUGUI health_bar;
    GameObject CanvasManager;
    // Start is called before the first frame update
    void Start()
    {
        player_health = max_health;
        CanvasManager = GameObject.FindObjectOfType<PauseScript1>().gameObject;

        go = false;
    }

    // Update is called once per frame
    void Update()
    {
        health_bar.text = "Health: " + player_health;
        if(go)
        {
            CanvasManager.GetComponent<PauseScript1>().endGame();
            Cursor.lockState = CursorLockMode.None;
            player_health = max_health;
            health_bar.text = "Health: " + player_health;
        }
    }
    public void damage(float amount)
    {
        player_health -= amount;
        if (player_health < 0)
        {
            go = true;
        }
    }
}
