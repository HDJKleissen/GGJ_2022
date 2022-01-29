using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    FMOD.Studio.EventInstance Music;
    // Start is called before the first frame update

    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Gameplay_Music");
        Music.start();
        Music.release();
    }

    private void Update()
    {
    }

    public void SetSpeedUp(bool value)
    {
        Music.setParameterByName("SpeedUp", value ? 1f : 0f, false);
    }

    public void SetOwner(bool value)
    {
        Music.setParameterByName("Owner", value ? 1f : 0f, false);
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
