using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI chickenTimeText, cleanTimeText, objectsBrokenText;
    public TextMeshProUGUI chickenTimeScoreText, cleanTimeScoreText, objectsBrokenScoreText, finalScoreText;

    public float CountTime;
    public float MaxDogTime;
    public int BrokenItemsMultiplier;

    int finalScore, objectsBroken, objectsScore, chickenScore, cleanScore;
    float chickenTime, cleanTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        chickenTimeText.SetText(FormatTime(chickenTime));
        cleanTimeText.SetText(FormatTime(cleanTime));
        objectsBrokenText.SetText(objectsBroken.ToString());
        chickenTimeScoreText.SetText(chickenScore.ToString());
        cleanTimeScoreText.SetText(cleanScore.ToString());
        objectsBrokenScoreText.SetText(objectsScore.ToString());
        finalScoreText.SetText(finalScore.ToString());
    }
    string FormatTime(float time)
    {
        return TimeSpan.FromSeconds(time).ToString("mm\\:ss\\.ff");
    }

    public void StartScoreScreen(ScoreObject scoreObject)
    {
        Sequence scoreSequence = DOTween.Sequence();

        int finalChickenScore = (int)Mathf.Clamp((MaxDogTime - scoreObject.DogTime)*1000, 0, MaxDogTime * 1000);
        int finalCleanScore = (int)Mathf.Clamp((GameController.Instance.OwnerTimeAmount - scoreObject.OwnerTime) * 1000, 0, GameController.Instance.OwnerTimeAmount * 1000) ;
        int finalObjectsScore = -scoreObject.BreakablesBroken * 1000;
        scoreObject.FinalScore = finalChickenScore + finalCleanScore + finalObjectsScore;
        //scoreSequence.AppendCallback(() => DingPlayer.Start());
        scoreSequence.Append(DOTween.To(() => chickenTime, time => chickenTime = time, scoreObject.DogTime, CountTime));
        scoreSequence.Append(DOTween.To(() => chickenScore, score => chickenScore = score, finalChickenScore, CountTime));
        //scoreSequence.AppendCallback(() => DingPlayer.Stop());
        scoreSequence.AppendInterval(CountTime);
        //scoreSequence.AppendCallback(() => DingPlayer.Start());
        scoreSequence.Append(DOTween.To(() => cleanTime, time => cleanTime = time, scoreObject.OwnerTime, CountTime));
        scoreSequence.Append(DOTween.To(() => cleanScore, score => cleanScore = score, finalCleanScore, CountTime));
        //scoreSequence.AppendCallback(() => DingPlayer.Stop());
        scoreSequence.AppendInterval(CountTime);
        //scoreSequence.AppendCallback(() => DingPlayer.Start());
        scoreSequence.Append(DOTween.To(() => objectsBroken, time => objectsBroken = time, scoreObject.BreakablesBroken, scoreObject.BreakablesBroken > 0 ? CountTime : 0));
        scoreSequence.Append(DOTween.To(() => objectsScore, score => objectsScore = score, finalObjectsScore, finalObjectsScore != 0 ? CountTime : 0));
        //scoreSequence.AppendCallback(() => DingPlayer.Stop());
        scoreSequence.AppendInterval(CountTime);
        //scoreSequence.AppendCallback(() => DingPlayer.Start());
        scoreSequence.Append(DOTween.To(() => finalScore, score => finalScore = score, scoreObject.FinalScore, CountTime));
        //scoreSequence.AppendCallback(() => DingPlayer.Stop());
        scoreSequence.AppendInterval(CountTime * 2);
        scoreSequence.OnComplete(() => {
            GameController.Instance.UIController.OpenHighScoreScene(scoreObject);
            gameObject.SetActive(false);
        });
    }
}
