using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject PauseMenu;
    public TextMeshProUGUI catchableUIText, timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            SetPause(true);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetPause(bool paused)
    {
        PauseMenu.SetActive(paused);
        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void UpdateTimer(float time)
    {
        timerText.SetText($"Timer: {time}");
    }

    public void UpdateCaughtChaseObjects(int caughtAmount, int total)
    {
        catchableUIText.SetText($"Chickens: {caughtAmount}/{total}");
    }
}
