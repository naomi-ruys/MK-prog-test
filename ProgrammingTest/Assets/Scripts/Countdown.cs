using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : Clock
{
    [SerializeField] private GameObject hours, minutes, seconds;
    [SerializeField] private GameObject separatorHM;
    [SerializeField] private Button startButton, stopButton;
    [SerializeField] private AudioSource alarmSound;

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

        if (currTime.second < MinimumHMS &&
            (currTime.minute > MinimumHMS || currTime.hour > MinimumHMS))
        {
            currTime.minute--;
            currTime.second = MaxSeconds;
        }

        if (currTime.minute < MinimumHMS && currTime.hour > MinimumHMS)
        {
            currTime.hour--;
            currTime.minute = MaxMinutes;
        }

        if (currTime.hour == MinimumHMS &&
            currTime.minute == MinimumHMS &&
            (int)currTime.second == MinimumHMS)
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
        if (currTime.hour == MinimumHMS)
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

        if(currTime.hour > MinimumHMS)
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
