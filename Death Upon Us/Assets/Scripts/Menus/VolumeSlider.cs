using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static utils.Configs;

public class VolumeSlider : MonoBehaviour
{
    //FMOD.Studio.EventInstance menu_theme;
    //float MusicVolume = 0.5f;

    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("sliderValue", DefaultVolume);
    }
    public void SetLevel(float sliderValue)
    {
        float volume = Mathf.Log10(sliderValue) * 20;
        PlayerPrefs.SetFloat("sliderValue", sliderValue);
    }

   /* public void volumeChange(float newVolume)
    {

    }*/
}
