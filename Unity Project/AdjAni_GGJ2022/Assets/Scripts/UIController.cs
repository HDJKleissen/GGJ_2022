using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject PauseMenu, GameOverMenuTemp;
    public TextMeshProUGUI catchableUIText, timerText, scoreSummaryText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.PlayerHasControl)
        {
            if (Input.GetButtonDown("Pause"))
            {
                SetPause(true);
            }
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
        timerText.SetText($"Timer: {FormatTime(time)}");
    }

    public void UpdateCaughtChaseObjects(int caughtAmount, int total)
    {
        catchableUIText.SetText($"Chickens: {caughtAmount}/{total}");
    }

    public void OpenEndScreen(ScoreObject scoreObject)
    {
        GameOverMenuTemp.SetActive(true);
        scoreSummaryText.SetText(
            $"You got all squirrels in <color=green>{ FormatTime(scoreObject.DogTime) }</color>. " +
            $"You broke <color=orange>{ scoreObject.BreakablesBroken }</color> objects, but could have broken <color=red>{ scoreObject.TotalBreakables }</color>. " +
            $"You fixed <color=green>{ scoreObject.BreakablesFixed }</color> of those with <color=green>{ FormatTime(scoreObject.OwnerTime) }</color> left. " +
            $"Well done!(or maybe not, i'm not smart enough to figure that out Sadge)");
    }

    string FormatTime(float time)
    {
        return TimeSpan.FromSeconds(time).ToString("mm\\:ss\\.ff");
    }
}