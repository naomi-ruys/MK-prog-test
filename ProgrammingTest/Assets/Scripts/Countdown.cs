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
        if(pause)
        {
            CancelInvoke();
        }

        //display time
        sText.text = currTime.ToString("ss");
        mText.text = currTime.ToString("mm");
        hText.text = currTime.ToString("HH");
    }

    private void DecrementTime()
    {
        currTime = currTime.AddSeconds(-1f);
        CheckEnd();
    }

    public void PauseTime()
    {
        pause = true;
        startButton.interactable = true;
        stopButton.interactable = false;
    }

    public void StartTime()
    {
        if (currTime.Hour == MinimumHMS)
        {
            hours.SetActive(false);
            separatorHM.SetActive(false);
        }

        InvokeRepeating(nameof(DecrementTime), 0, 1f);
        pause = false;

        startButton.interactable = false;
        stopButton.interactable = true;
    }

    public override void SetTime(System.DateTime newTime, bool am = true)
    {
        currTime = newTime;

        if(currTime.Hour > MinimumHMS)
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
        if (currTime.Hour > MinimumHMS ||
            currTime.Minute > MinimumHMS ||
            currTime.Second > MinimumHMS)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }

    private void CheckEnd()
    {
        if (currTime.Hour == MinimumHMS &&
            currTime.Minute == MinimumHMS &&
            currTime.Second == MinimumHMS)
        {
            pause = true;
            startButton.interactable = false;
            stopButton.interactable = false;
            PlaySound();
        }
    }

    private void PlaySound()
    {
        alarmSound.PlayOneShot(alarmSound.clip);
    }

}
