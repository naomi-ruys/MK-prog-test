using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Clock : MonoBehaviour
{
    public const int MaxSeconds = 59;
    public const int MaxMinutes = 59;
    public const int MaxHours12 = 12;
    public const int MaxHours24 = 23;
    public const int MinHours12 = 1;
    public const int MinimumHMS = 0;

    public const string AMString = "AM";
    public const string PMString = "PM";

    [SerializeField] private Button removeButton;
    protected System.DateTime currTime = new System.DateTime(1, 1, 1, 0, 0, 0, 0);

    //start paused, allow player to begin stopwatch/countdown
    protected bool pause = true;

    public virtual void SetTime(System.DateTime newTime, bool am = true)
    {
        currTime = newTime;
    }

    public void OpenSetTimePanel(Clock display)
    {
        ClockManager.cm.OpenSetTimePanel(display);
    }

    public System.DateTime GetTime()
    {
        return currTime;
    }

    public virtual int GetHour()
    {
        return int.Parse(currTime.ToString("HH"));
    }

    public int GetMinute()
    {
        return currTime.Minute;
    }

    public int GetSecond()
    {
        return currTime.Second;
    }

    public void RemoveClock()
    {
        ClockManager.cm.RemoveClock(this);
        Destroy(gameObject);
    }

    public void DeactiveRemoveButton()
    {
        removeButton.interactable = false;
    }

    public void ActiveRemoveButton()
    {
        removeButton.interactable = true;
    }

    public virtual bool GetAM()
    {
        return false;
    }

    public virtual bool Get12hr()
    {
        return false;
    }
}
