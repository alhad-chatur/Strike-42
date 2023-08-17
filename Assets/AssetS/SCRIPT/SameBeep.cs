using UnityEngine;

public class SameBeep : MonoBehaviour
{
    public AudioSource AudioSource;
    private float musicV = 1f;
    void Start()
    {
        AudioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        AudioSource.volume = musicV;
    }
    public void updateVolume(float volume)
    {
        musicV = volume;
    }
}
