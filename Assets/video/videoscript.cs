using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class videoscript : MonoBehaviour
{
    VideoPlayer vp;
    // Start is called before the first frame update
    void Start()
    {
        vp = this.GetComponent<VideoPlayer>();
    }
    void Update()
    {
        if(vp.time>=42.0f ||(Input.GetKey(KeyCode.LeftShift)&&Input.GetKey(KeyCode.D)))
         SceneManager.LoadScene("MainGame");
    }
}
