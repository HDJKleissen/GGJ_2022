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
        MusicPlayer.Instance.SetMenu(true);
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
