using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public AudioSource musicIntro;
    public AudioSource musicLoop;
    [SerializeField] bool partialLoop = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!partialLoop)
        {
            musicLoop.Play();
        }
        else
        {
            musicIntro.Play();
            musicLoop.PlayDelayed(musicIntro.clip.length);
        }
    }

    

    public void StopMusic()
    {
        musicIntro.Stop();
        musicLoop.Stop();
    }
}
