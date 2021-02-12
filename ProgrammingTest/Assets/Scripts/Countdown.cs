using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : Clock
{
    public GameObject hours, minutes, seconds;
    public GameObject separatorHM, separatorMS;
    public Button startButton, stopButton;
    public AudioSource alarmSound;

    private Text hText, mText, sText;

    void Start()
    {
        //get all text elements
        hText = hours.GetComponentInChildren<Text>();
        mText = minutes.GetComponentInChildren<Text>();
        sText = seconds.GetComponentInChildren<Text>();

        //only interact with start/stop once a time has been set
        startButton.interactable = false;
        stopButton.interactable = false;
    }

    void Update()
    {
        if (!pause)
        {
            DecrementTime();
        }

        //display time
        sText.text = Util.DoubleDigit((int)currTime.second);
        mText.text = Util.DoubleDigit(currTime.minute);
        hText.text = Util.DoubleDigit(currTime.hour);
    }

    private void DecrementTime()
    {
        currTime.second -= Time.deltaTime;

        if (currTime.second < 0 && (currTime.minute > 0 || currTime.hour > 0))
        {
            currTime.minute--;
            currTime.second = 59;
        }

        if (currTime.minute < 0 && currTime.hour > 0)
        {
            currTime.hour--;
            currTime.minute = 59;
        }

        if (currTime.hour == 0 && currTime.minute == 0 && (int)currTime.second == 0)
        {
            pause = true;
            startButton.interactable = false;
            PlaySound();
        }
    }

    public void PauseTime()
    {
        pause = true;
        startButton.interactable = true;
        stopButton.interactable = false;
    }

    public void StartTime()
    {
        if (currTime.hour == 0)
        {
            hours.SetActive(false);
            separatorHM.SetActive(false);
        }
        pause = false;

        startButton.interactable = false;
        stopButton.interactable = true;
    }

    public override void SetTime(Util.MyTime newTime, bool am = true)
    {
        currTime = newTime;

        if(currTime.hour > 0)
        {
            hours.SetActive(true);
            separatorHM.SetActive(true);
        }
        else
        {
            hours.SetActive(false);
            separatorHM.SetActive(false);
        }

        //only allow interaction with start if the timer is not set to 00:00:00
        if (!currTime.TimeIsZero())
        {
            startButton.interactable = true;
        }
    }

    private void PlaySound()
    {
        alarmSound.PlayOneShot(alarmSound.clip);
    }

}
