using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Music : MonoBehaviour
{
    public static Background_Music instance;
    AudioSource audioSource;
    public AudioClip[] audioClips;
    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        MenuMusic();
    }

    public void MenuMusic()
    {
        audioSource.clip = audioClips[0];
        audioSource.Stop();
        audioSource.Play();
    }

    public void Level1Music()
    {   
        audioSource.clip = audioClips[1];
        audioSource.Stop();
        audioSource.Play();
    }
}
