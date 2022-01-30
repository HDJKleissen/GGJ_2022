using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using UnityEngine.UI;

public class BusController : MonoBehaviour
{
    Bus Bus;
    public string BusPath;
    private float BusVolume;
    private float FinalBusVolume;
    private Slider Slider;
    EventInstance LevelTest;
    PLAYBACK_STATE pb;

    // Start is called before the first frame update
    void Start()
    {
        Bus = FMODUnity.RuntimeManager.GetBus("bus:/" + BusPath);
        Bus.getVolume(out BusVolume, out FinalBusVolume);


        Slider = GetComponent<Slider>();
        //Slider.value = BusVolume;

        if(BusPath == "SFX")
        {
            LevelTest = FMODUnity.RuntimeManager.CreateInstance("event:/Bark_for_UI");
        }
        else
            LevelTest.release(); 
    }

    public void VolumeLevel(float SliderValue)
    {
        Bus.setVolume(SliderValue);
        PlayerPrefs.SetFloat(BusPath + "Volume", SliderValue);
        if(BusPath == "SFX")
        {
            LevelTest.getPlaybackState(out pb);
            if (pb != PLAYBACK_STATE.PLAYING)
                LevelTest.start();
        }
    }

    private void OnDestroy()
    {
        if (BusPath == "SFX") 
            LevelTest.release();
    }
}
