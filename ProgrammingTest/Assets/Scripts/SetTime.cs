using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTime : MonoBehaviour
{
    [SerializeField] private GameObject hours, minutes, seconds;
    [SerializeField] private GameObject separatorHM, separatorMS, amPM;

    private Util.MyTime newTime = new Util.MyTime(0, 0, 0);
    private Clock display;
    private bool format12hr = true;
    private bool formatAM = false;
    private bool timeDisplay;

    private Text hText, mText, sText, amText;

    // Start is called before the first frame update
    void Start()
    {
        //get all text elements
        hText = hours.GetComponentInChildren<Text>();
        mText = minutes.GetComponentInChildren<Text>();
        sText = seconds.GetComponentInChildren<Text>();
        amText = amPM.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //display time
        sText.text = Util.DoubleDigit((int)newTime.second);
        mText.text = Util.DoubleDigit(newTime.minute);
        hText.text = Util.DoubleDigit(newTime.hour);

        if (formatAM)
        {
            amText.text = Clock.AMString;
        }
        else
        {
            amText.text = Clock.PMString;
        }
    }

    public void CloseSetTimePanel()
    {
        display.SetTime(newTime, formatAM);
        gameObject.SetActive(false);
    }

    public void StartSetTime(Clock display)
    {
        this.display = display;
        newTime = display.GetTime();

        if (display.GetType().ToString() == "TimeDisplay")
        {
            timeDisplay = true;
            format12hr = display.Get12hr();
            formatAM = display.GetAM();
        }
        else
        {
            timeDisplay = false;
        }

        gameObject.SetActive(true);

        if (timeDisplay)
        {
            if (format12hr)
            { //12hr display
                separatorMS.SetActive(false);
                seconds.SetActive(false);
                amPM.SetActive(true);
            }
            else
            { //24hr display
                separatorMS.SetActive(false);
                seconds.SetActive(false);
                amPM.SetActive(false);
            }
        }
        else
        { //countdown display
            separatorMS.SetActive(true);
            seconds.SetActive(true);
            amPM.SetActive(false);
        }
    }

    // ----- Time Change Buttons ---------------

    public void IncrementHours()
    {
        newTime.hour++;
        if(timeDisplay && format12hr && newTime.hour > Clock.MaxHours12)
        {
            newTime.hour = Clock.MinHours12;
        }
        else if (newTime.hour > Clock.MaxHours24)
        {
            newTime.hour = Clock.MinimumHMS;
        }
    }

    public void DecrementHours()
    {
        newTime.hour--;
        if (timeDisplay && format12hr && newTime.hour < Clock.MinHours12)
        {
            newTime.hour = Clock.MaxHours12;
        }
        else if (newTime.hour < Clock.MinimumHMS)
        {
            newTime.hour = Clock.MaxHours24;
        }
    }

    public void IncrementMinutes()
    {
        newTime.minute++;
        if(newTime.minute > Clock.MaxMinutes)
        {
            newTime.minute = Clock.MinimumHMS;
        }
    }

    public void DecrementMinutes()
    {
        newTime.minute--;
        if (newTime.minute < Clock.MinimumHMS)
        {
            newTime.minute = Clock.MaxMinutes;
        }
    }

    public void IncrementSeconds()
    {
        newTime.second++;
        if (newTime.second > Clock.MaxSeconds)
        {
            newTime.second = Clock.MinimumHMS;
        }
    }

    public void DecrementSeconds()
    {
        newTime.second--;
        if (newTime.second < Clock.MinimumHMS)
        {
            newTime.second = Clock.MaxSeconds;
        }
    }

    public void ToggleAmPm()
    {
        formatAM = !formatAM;
    }

}
