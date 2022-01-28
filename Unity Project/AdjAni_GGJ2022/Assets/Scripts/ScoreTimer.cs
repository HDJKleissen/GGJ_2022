using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreTimer : MonoBehaviour
{
    private TextMeshProUGUI scoreTmp;
    [SerializeField] private float time;
    private bool timerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreTmp = GetComponent<TextMeshProUGUI>();
        StartStopwatch();
    }

    public void StartStopwatch()
    {
        time = 0;
        timerActive = true;
    }

    public void StopStopwatch()
    {
        timerActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            time += Time.deltaTime;
        }

        //unefficient but w/e
        string timeFormatted = TimeSpan.FromSeconds(time).ToString("mm\\:ss\\.ff");
        scoreTmp.text = timeFormatted;
    }
}
