using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawn_enemies : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public TextMeshProUGUI score;
    float a=20f;

    public int score_board = -200;
    List<Transform> check_points = new List<Transform>();
    
    public int count;
   
   public GameObject[] Enemies;
    public int max_length=2;

    public void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject t in obj)
        {
            check_points.Add(t.transform);
        }
    }
   public void Start()
    {
        // Instantiate(Enemies[Random.Range(0, Enemies.Length)], check_points[Random.Range(0, check_points.Count)].position, Quaternion.identity);
        Instantiate(Enemies[0], check_points[Random.Range(0, check_points.Count)].position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(wavecounter);

        score.text = "Score: " + score_board;
        if (max_length < 64)
        {
            a -= Time.deltaTime;
            timer.text = "Enemies double after: " + a + " sec";
        }
        else
        {
            timer.text = "Enemies maintained static at 64... ";
        }

        if (a<=0)
        {
            if (max_length <= 64)
            {
                max_length = max_length * 2;
            }
            a = 20f;
        }
        
        //gos = GameObject.FindGameObjectsWithTag("Respawn");
        count = GameObject.FindGameObjectsWithTag("Respawn").Length;
        //Debug.Log(count);
        spawn(count);
    }
    void spawn(int counter)
    {
        if(counter<max_length)
        {
            //Instantiate(Enemies[Random.Range(0, Enemies.Length)], check_points[Random.Range(0, check_points.Count)].position, Quaternion.identity);
           // score_board += 100;
        }
        else
        {
            return;
        }
        
        
    }

}
