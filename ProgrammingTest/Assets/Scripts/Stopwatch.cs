using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : Clock
{
    public GameObject minutes, seconds, milliSeconds;
    public Button startButton, stopButton, resetButton;

    private Text mText, sText, msText;

    private const int MaxMilliSeconds = 99;
    private const int MaxMinutesStopwatch = 99;

    void Start()
    {
        //get all text elements
        mText = minutes.GetComponentInChildren<Text>();
        sText = seconds.GetComponentInChildren<Text>();
        msText = milliSeconds.GetComponentInChildren<Text>();
        startButton.interactable = true;
        stopButton.interactable = false;
    }

    void Update()
    {
        if (!pause)
        {
            IncrementTime();
        }

        //display time
        msText.text = Util.DoubleDigit((int)currTime.milliSecond);
        sText.text = Util.DoubleDigit((int)currTime.second);
        mText.text = Util.DoubleDigit(currTime.minute);
    }

    private void IncrementTime()
    {
        currTime.milliSecond += Time.deltaTime * 100;

        if ((int)currTime.milliSecond > MaxMilliSeconds)
        {
            currTime.second++;
            currTime.milliSecond = MinimumHMS;
        }

        if (currTime.second > MaxSeconds)
        {
            currTime.minute++;
            currTime.second = MinimumHMS;
        }

        if (currTime.minute >= MaxMinutesStopwatch &&
            currTime.second == MaxSeconds &&
            (int)currTime.milliSecond == MaxMilliSeconds) 
        {
            pause = true;
            startButton.interactable = false;
        }

    }

    public void PauseTime()
    {
        pause = true;
        startButton.interactable = true;
        stopButton.interactable = false;
        resetButton.interactable = true;
    }

    public void StartTime()
    {
        pause = false;
        startButton.interactable = false;
        stopButton.interactable = true;
        resetButton.interactable = false;
    }

    public void ResetTime()
    {
        currTime = new Util.MyTime(0, 0, 0, 0);
        startButton.interactable = true;
        stopButton.interactable = false;
    }

}
