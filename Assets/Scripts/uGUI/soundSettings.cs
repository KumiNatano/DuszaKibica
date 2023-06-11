using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class soundSettings : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioMixer masterMixer;

    private void Start()
    {
        SetMusiCVolume(PlayerPrefs.GetFloat("SavedMusicVolume", 100));
        SetSFXVolume(PlayerPrefs.GetFloat("SavedSFXVolume", 100));
    }

    public void SetSFXVolume(float value)
    {
        if (value < 1) {
            value = 0.001f;
        }
        RefreshSFXSlider(value);
        PlayerPrefs.SetFloat("SavedSFXVolume", value);
        masterMixer.SetFloat("SFX", Mathf.Log10(value / 100) * 20f);
        
    }

    public void SetSFXVolumeFromSlider()
    {
        SetSFXVolume(sfxSlider.value);
    }


    public void RefreshSFXSlider(float value)
    {
        sfxSlider.value = value;
    }
    
    //---
    public void SetMusiCVolume(float value)
    {
        if (value < 1) {
            value = 0.001f;
        }
        RefreshMusicSlider(value);
        PlayerPrefs.SetFloat("SavedMusicVolume", value);
        masterMixer.SetFloat("Music", Mathf.Log10(value / 100) * 20f);
        
    }

    public void SetMusicVolumeFromSlider()
    {
        SetMusiCVolume(musicSlider.value);
    }


    public void RefreshMusicSlider(float value)
    {
        musicSlider.value = value;
    }
}
