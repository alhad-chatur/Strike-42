using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDead : MonoBehaviour
{
    public  bool IsPlayerDead = false;
    public GameObject Openmenu;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerDead)
        {
            Openmenu.SetActive(true);
        }
        else
        {
            Openmenu.SetActive(false);
        }

    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
