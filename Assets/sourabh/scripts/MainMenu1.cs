using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(Time.timeScale);
    }
    public void Menuupdate()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}
