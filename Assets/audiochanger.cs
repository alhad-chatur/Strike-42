using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiochanger : MonoBehaviour
{
    // Start is called before the first frame update
    Spawn_script a;
    public AudioClip genaudio;
    public AudioClip silentaudio;
    AudioSource asur;
    [SerializeField]
    LayerMask lm;
    Collider[] c;
    void Start()
    {
        a = FindObjectOfType<Spawn_script>();
        asur = GetComponent<AudioSource>();

        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (a.switchingWave == false && asur.clip != genaudio)
        {
            asur.clip = genaudio;
            asur.Play();
        }
        if (a.switchingWave == true && asur.clip != silentaudio)
        {
            asur.clip = silentaudio;
            asur.Play();
        }
        if (Physics.OverlapSphere(this.transform.position, 50.0f, lm).Length == 0)
        {

            if (asur.clip == genaudio)
            {


                asur.volume = 0.1f;

            }
            else
                asur.volume = 1.0f;
        }
        else
            asur.volume = 1.0f;

    }
}
