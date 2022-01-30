using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetValuesFromPlayerPrefs : MonoBehaviour
{
    public Slider MusicSlider, SFXSlider;
    public Toggle ScreenshakeToggle;

    // Start is called before the first frame update
    void Start()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        ScreenshakeToggle.isOn = PlayerPrefs.GetInt("ScreenShakeEnabled", 1) == 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
