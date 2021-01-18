using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTime : MonoBehaviour
{
    public GameObject hours, minutes, seconds;
    public GameObject separatorHM, separatorMS;
    public GameObject amPM;

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
            amText.text = "AM";
        }
        else
        {
            amText.text = "PM";
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
        if(timeDisplay && format12hr && newTime.hour > 12)
        {
            newTime.hour = 1;
        }
        else if (newTime.hour > 23)
        {
            newTime.hour = 0;
        }
    }

    public void DecrementHours()
    {
        newTime.hour--;
        if (timeDisplay && format12hr && newTime.hour < 1)
        {
            newTime.hour = 12;
        }
        else if (newTime.hour < 0)
        {
            newTime.hour = 23;
        }
    }

    public void IncrementMinutes()
    {
        newTime.minute++;
        if(newTime.minute > 59)
        {
            newTime.minute = 0;
        }
    }

    public void DecrementMinutes()
    {
        newTime.minute--;
        if (newTime.minute < 0)
        {
            newTime.minute = 59;
        }
    }

    public void IncrementSeconds()
    {
        newTime.second++;
        if (newTime.second > 59)
        {
            newTime.second = 0;
        }
    }

    public void DecrementSeconds()
    {
        newTime.second--;
        if (newTime.second < 0)
        {
            newTime.second = 59;
        }
    }

    public void ToggleAmPm()
    {
        formatAM = !formatAM;
    }

}
