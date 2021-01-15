using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : Clock
{
    public GameObject hours, minutes, seconds;
    public GameObject separatorHM, separatorMS;
    public Button startButton;
    public AudioSource alarmSound;

    private Util.MyTime initTime = new Util.MyTime(0, 0, 10);
    private Util.MyTime currTime = new Util.MyTime(0, 0, 10);
    private bool pause = true;

    private Text hText, mText, sText;

    void Start()
    {
        //get all text elements
        hText = hours.GetComponentInChildren<Text>();
        mText = minutes.GetComponentInChildren<Text>();
        sText = seconds.GetComponentInChildren<Text>();
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

        if (currTime.second < 0 && currTime.minute > 0)
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

    public void PauseTime() => pause = true;

    public void StartTime()
    {
        pause = false;
        //if hrs == 0 setactive false, for hrs & sepHM
    }

    public void SetTime()
    {
        currTime = initTime;
        //do the change
        //cap at 99:59:59

        startButton.interactable = true;
    }

    private void PlaySound()
    {
        alarmSound.PlayOneShot(alarmSound.clip);
    }

}
