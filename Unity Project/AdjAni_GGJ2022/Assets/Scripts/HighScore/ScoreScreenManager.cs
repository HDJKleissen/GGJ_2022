using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject inputScreen;
    [SerializeField] private GameObject highScoreScreen;

    [SerializeField] private TextMeshProUGUI nameTmpro;
    HighScores highScores;
    // Start is called before the first frame update
    void Start()
    {
        highScores = GetComponent<HighScores>();
    }

    public void GoHighScoreScene()
    {
        if (IsHighScore())
        {
            inputScreen.SetActive(true);
            highScoreScreen.SetActive(false);
        }
        else
        {
            GoHighScoreScreen();
        }
    }

    public void GoHighScoreScreen()
    {
        inputScreen.SetActive(false);
        highScoreScreen.SetActive(true);
    }

    //gets called from the continue button lol
    public void UploadScore()
    {
        //also automatically downloads score..
        highScores.UploadScore(nameTmpro.text, (int)GameController.Instance.scoreObject.FinalScore);
    }

    private bool IsHighScore()
    {
        //if top 10 ask for name
        if (highScores.scoreList.Length < 10 || GameController.Instance.scoreObject.FinalScore > highScores.scoreList[9].score)
        {
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
