using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static utils.Configs;

public class VolumeSlider : MonoBehaviour
{
    FMOD.Studio.Bus masterBus;

    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("sliderValue", DefaultVolume);
        masterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
    }

    void Update(){
        masterBus.setVolume(slider.value);
    }
    public void SetLevel(float sliderValue)
    {
        float volume = Mathf.Log10(sliderValue) * 20;
        PlayerPrefs.SetFloat("sliderValue", sliderValue);
    }
}
