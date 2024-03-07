using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }
    public void LaserHit()
    {
        audioSource.PlayOneShot(audioClips[1]);
    }
    public void Crash()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }
    public void GameOverSound()
    {
        audioSource.PlayOneShot(audioClips[3]);
    }

}
