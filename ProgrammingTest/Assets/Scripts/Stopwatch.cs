using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : Clock
{
    [SerializeField] private GameObject minutes, seconds, milliSeconds;
    [SerializeField] private Button startButton, stopButton, resetButton;

    private Text mText, sText, msText;

    private const int MaxMilliseconds = 990;

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
        if (pause)
        {
            CancelInvoke();
        }

        //display time
        msText.text = currTime.ToString("ff");
        sText.text = currTime.ToString("ss");
        mText.text = currTime.ToString("mm");
    }

    private void IncrementTime()
    {
        currTime = currTime.AddMilliseconds(10f);
        CheckMax();
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
        InvokeRepeating(nameof(IncrementTime), 0, .01f);
        pause = false;
        startButton.interactable = false;
        stopButton.interactable = true;
        resetButton.interactable = false;
    }

    public void ResetTime()
    {
        currTime = new System.DateTime(1, 1, 1, 0, 0, 0, 0);
        startButton.interactable = true;
        stopButton.interactable = false;
    }

    private void CheckMax()
    {
        if (currTime.Minute == MaxMinutes &&
            currTime.Second == MaxSeconds &&
            currTime.Millisecond == MaxMilliseconds)
        {
            pause = true;
            startButton.interactable = false;
        }
    }
}

