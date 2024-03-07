using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public static float musicVolume;
    public static float SFXVolume;

    void Update()
    {
        musicSlider.value = musicVolume;
        sfxSlider.value = SFXVolume;
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        musicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        SFXVolume = volume;
    }
}
