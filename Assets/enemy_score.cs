using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class enemy_score : MonoBehaviour
{
    [SerializeField]
    int score = 0;

    public int currentScore=0;

    
    public TextMeshProUGUI text;


    private void Start()
    {
        text = GameObject.FindGameObjectWithTag("score").GetComponent<TextMeshProUGUI>();
        //text.text = "Score: 0";
    }

    public void scoreUpdate(GameObject G)
    {

        currentScore = int.Parse(text.text.Substring(7));
        //Debug.Log(currentScore);
        Debug.Log(G.name);
        currentScore += score;
        text.text = "Score: " + currentScore.ToString();
    }
}
