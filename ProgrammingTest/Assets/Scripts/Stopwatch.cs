using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : Clock
{
    public GameObject minutes, seconds, milliSeconds;
    public Button startButton, stopButton, resetButton;

    private Text mText, sText, msText;

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

        if (currTime.milliSecond > 100)
        {
            currTime.second++;
            currTime.milliSecond = 0;
        }

        if (currTime.second > 59)
        {
            currTime.minute++;
            currTime.second = 0;
        }

        if (currTime.minute >= 99 && currTime.second == 59 && (int)currTime.milliSecond == 99) 
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
