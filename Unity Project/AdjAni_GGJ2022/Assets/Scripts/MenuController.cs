using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public 
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            MusicPlayer.Instance.SetMenu(true);
            MusicPlayer.Instance.SetSpeedUp(false);
            MusicPlayer.Instance.SetOwner(false);
            MusicPlayer.Instance.SetWin(false);
        }
        Bus SFXBus = FMODUnity.RuntimeManager.GetBus("bus:/" + "SFX");
        Bus MusicBus = FMODUnity.RuntimeManager.GetBus("bus:/" + "Music");

        SFXBus.setVolume(PlayerPrefs.GetFloat("SFXVolume",1));
        MusicBus.setVolume(PlayerPrefs.GetFloat("MusicVolume", 1));
    }

    public void UnMenuMusic()
    {
        MusicPlayer.Instance.SetMenu(false);
        MusicPlayer.Instance.SetSpeedUp(false);
        MusicPlayer.Instance.SetOwner(false);
        MusicPlayer.Instance.SetWin(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
