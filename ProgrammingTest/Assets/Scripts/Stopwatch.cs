using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    public GameObject minutes, seconds, milliSeconds;
    public Button startButton, removeButton;

    private Util.MyTime currTime = new Util.MyTime(0, 99, 55, 0);
    //start paused, allow player to begin stopwatch
    private bool pause = true;

    private Text mText, sText, msText;

    void Start()
    {
        //get all text elements
        mText = minutes.GetComponentInChildren<Text>();
        sText = seconds.GetComponentInChildren<Text>();
        msText = milliSeconds.GetComponentInChildren<Text>();
        startButton.interactable = true;
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

        if (currTime.minute >= 99 && currTime.second == 59 && currTime.milliSecond >= 99) 
        {
            pause = true;
            startButton.interactable = false;
            //change colour
        }

    }

    public void PauseTime() => pause = true;

    public void StartTime() => pause = false;

    public void ResetTime()
    {
        currTime = new Util.MyTime(0, 0, 0, 0);
        startButton.interactable = true;
    }

    public void RemoveClock()
    {
        Destroy(gameObject);
    }

    public void DeactiveRemoveButton()
    {
        removeButton.interactable = false;
    }
}
