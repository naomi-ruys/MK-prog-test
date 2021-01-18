using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Clock : MonoBehaviour
{
    public Button removeButton;
    protected Util.MyTime currTime = new Util.MyTime(0, 0, 0);

    //start paused, allow player to begin stopwatch
    protected bool pause = true;

    virtual public void SetTime(Util.MyTime newTime, bool am = true)
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

    virtual public bool GetAM()
    {
        return false;
    }

    virtual public bool Get12hr()
    {
        return false;
    }
}
