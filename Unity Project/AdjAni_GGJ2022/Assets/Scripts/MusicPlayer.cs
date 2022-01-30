using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : UnitySingleton<MusicPlayer>
{

    FMOD.Studio.EventInstance Music;
    // Start is called before the first frame update

    public bool Menu, Speedup, Win, Owner;

    void Start()
    {
        if (Instance == this)
        {
            Music = FMODUnity.RuntimeManager.CreateInstance("event:/Gameplay_Music");
            Music.start();
            Music.release();
            DontDestroyOnLoad(this);
            SetSpeedUp(Speedup);
            SetWin(Win);
            SetOwner(Owner);
            SetMenu(Menu);
        }
    }

    private void Update()
    {
    }

    public void SetSpeedUp(bool value)
    {
        Music.setParameterByName("SpeedUp", value ? 1f : 0f, false);
        Speedup = value;
    }

    public void SetOwner(bool value)
    {
        Music.setParameterByName("Owner", value ? 1f : 0f, false);
        Owner = value;
    }

    public void SetMenu(bool value)
    {
        Music.setParameterByName("Menu", value ? 1f : 0f, false);
        Menu = value;
    }

    public void SetWin(bool value)
    {
        Music.setParameterByName("Win", value ? 1f : 0f, false);
        Win = value;
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
