using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Clock : MonoBehaviour
{
    public Button removeButton;
    protected Util.MyTime currTime = new Util.MyTime(0, 0, 0);

    public const int MaxSeconds = 59;
    public const int MaxMinutes = 59;
    public const int MaxHours12 = 12;
    public const int MaxHours24 = 23;
    public const int MinHours12 = 1;
    public const int MinimumHMS = 0;

    public const string AMString = "AM";
    public const string PMString = "PM";

    //start paused, allow player to begin stopwatch
    protected bool pause = true;

    public virtual void SetTime(Util.MyTime newTime, bool am = true)
    {
        currTime = newTime;
    }

    public void OpenSetTimePanel(Clock display)
    {
        ClockManager.cm.OpenSetTimePanel(display);
    }

    public Util.MyTime GetTime()
    {
        return currTime;
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
