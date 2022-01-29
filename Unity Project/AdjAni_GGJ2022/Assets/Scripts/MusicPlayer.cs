using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    FMOD.Studio.EventInstance Music;
    public bool fast;
    public bool owner;

    // Start is called before the first frame update

    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Gameplay_Music");
        Music.start();
        Music.release();
    }

    private void Update()
    {
        SetSpeedUp();
        SetOwner();
    }

    void SetSpeedUp()
    {
        if (fast == true)
        {
            Music.setParameterByName("SpeedUp", 1f, false);
        }
        else
        {
            Music.setParameterByName("SpeedUp", 0f, false);
        }
    }

    void SetOwner()
    {
        if (owner == true)
        {
            Music.setParameterByName("Owner", 1f, false);
        }
        else
        {
            Music.setParameterByName("Owner", 0f, false);
        }

    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
