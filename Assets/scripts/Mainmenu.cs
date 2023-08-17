using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void Menuupdate()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}
