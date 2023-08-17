using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PauseScript1 : MonoBehaviour
{
    public static bool IsGamePaused = false;
    [SerializeField]private GameObject Pausemenu;
    [SerializeField]private GameObject InGameUI;
    [SerializeField] private GameObject EndMenu;
    [SerializeField] TextMeshProUGUI healthrestore;
    GameObject[] health_Restores ;
    int MaxHr;
    int currentHRcount = 0;
    private TextMeshProUGUI FinalScoreText;
    private GameObject activeWepon;
    bool GameEnded;

    private void Start()
    {
        health_Restores = GameObject.FindGameObjectsWithTag("hr");
        MaxHr = health_Restores.Length;
        GameEnded = false;
        Cursor.lockState = CursorLockMode.Locked;
        activeWepon = GameObject.FindGameObjectWithTag("Player").GetComponent<switching>().activeWeponObject;
    }
    void Update()
    {
        health_Restores = GameObject.FindGameObjectsWithTag("hr");
        currentHRcount = health_Restores.Length;
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<health_manager>().player_health < 300)
        {
            healthrestore.enabled = true;
            healthrestore.text = currentHRcount.ToString() + " / " + MaxHr.ToString() + " HEALTH REStORE LEFT...Find Them All...";
        }
        else {

            healthrestore.enabled = false;
        }
       
          activeWepon = GameObject.FindGameObjectWithTag("Player").GetComponent<switching>().activeWeponObject;
        if (Input.GetKeyDown(KeyCode.Escape) && !GameEnded)
        {
            if (IsGamePaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Resume();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Paused();
            }
        }
    }
    public void Resume()
    {
        Debug.Log("Resumed");
        Time.timeScale = 1f;
        IsGamePaused = false;
        Pausemenu.SetActive(false);
        InGameUI.SetActive(true);
        activeWepon.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    public void Paused()
    {
        Time.timeScale = 0f;
        activeWepon.SetActive(false);
        InGameUI.SetActive(false);
        Pausemenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        IsGamePaused = true;
    }

    public void LoadMenu()
    {
        GameEnded = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("outpost on desert");
    }

    public void endGame()
    {

        Cursor.lockState = CursorLockMode.None;
        activeWepon.SetActive(false);
        EndMenu.SetActive(true);
        GameObject.FindGameObjectWithTag("EndScore").GetComponent<TextMeshProUGUI>().text = GameObject.FindGameObjectWithTag("score").GetComponent<TextMeshProUGUI>().text;
        InGameUI.SetActive(false);
        Pausemenu.SetActive(false);
        GameEnded = true;
        Time.timeScale = 0f;


    }

    public void RePlay()
    {
        GameEnded = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainGame");
    }
}

