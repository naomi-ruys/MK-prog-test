using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : Clock
{
    public GameObject hours, minutes, seconds;
    public GameObject separatorHM, separatorMS;
    public GameObject amPM;

    private bool format12hr = false;
    private bool formatAM;

    private Text hText, mText, sText, amText;

    void Start()
    {
        //get all text elements
        hText = hours.GetComponentInChildren<Text>();
        mText = minutes.GetComponentInChildren<Text>();
        sText = seconds.GetComponentInChildren<Text>();
        amText = amPM.GetComponentInChildren<Text>();

        //set initial time as current time
        currTime.hour = System.DateTime.Now.Hour;
        currTime.minute = System.DateTime.Now.Minute;
        currTime.second = System.DateTime.Now.Second;
        //get the AM/PM of the current time
        string tt = System.DateTime.Now.ToString(
            "tt", System.Globalization.CultureInfo.InvariantCulture);

        if (tt == "AM")
        {
            formatAM = true;
        }
        else
        {
            formatAM = false;
        }

        Format12();
    }

    void Update()
    {
        IncrementTime();

        //display time
        sText.text = Util.DoubleDigit((int)currTime.second);
        mText.text = Util.DoubleDigit(currTime.minute);
        hText.text = Util.DoubleDigit(currTime.hour);

        displayAmPm();
    }

    private void IncrementTime()
    {
        currTime.second += Time.deltaTime;

        if (currTime.second > 60)
        {
            currTime.minute++;
            currTime.second = 0;
        }

        if (currTime.minute > 59)
        {
            currTime.hour++;
            currTime.minute = 0;

            //change AM/PM
            if (currTime.hour == 12 || currTime.hour == 24)
            {
                ToggleAmPm();
            }
        }

        if (format12hr && currTime.hour > 12)
        {
            currTime.hour = 1;
        }
        else if (!format12hr && currTime.hour > 23)
        {
            currTime.hour = 0;
        }
    }

    override public void SetTime(Util.MyTime newTime, bool am = true)
    {
        currTime = newTime;
        formatAM = am;

        displayAmPm();
    }

    public void FormatChange(int format)
    {
        switch (format)
        {
            case 0:         //12hr hh:mm
                RemoveSeconds();
                Format12();
                break;
            case 1:         //12hr hh:mm:ss
                ActivateSeconds();
                Format12();
                break;
            case 2:         //24hr hh:mm
                RemoveSeconds();
                Format24();
                break;
            case 3:         //24hr hh:mm:ss
                ActivateSeconds();
                Format24();
                break;
            default:
                break;
        }
    }

    private void RemoveSeconds()
    {
        separatorMS.SetActive(false);
        seconds.SetActive(false);
    }

    private void ActivateSeconds()
    {
        separatorMS.SetActive(true);
        seconds.SetActive(true);
    }

    private void Format24()
    {
        //Don't reformat 12/24hr if just toggling seconds
        if (!format12hr)
        {
            return;
        }

        amPM.SetActive(false);
        format12hr = false;

        //time change for PM
        if (!formatAM && currTime.hour != 12)
        {
            currTime.hour += 12;
        }
        else if (formatAM && currTime.hour == 12)
        {
            currTime.hour = 0;
        }
    }

    private void Format12()
    {
        //Don't reformat 12/24hr if just toggling seconds 
        if (format12hr)
        {
            return;
        }

        amPM.SetActive(true);
        format12hr = true;

        if (currTime.hour > 12)
        {
            currTime.hour -= 12;
        }
        else if (currTime.hour == 0)
        {
            currTime.hour = 12;
        }
    }

    private void ToggleAmPm()
    {
        formatAM = !formatAM;
    }

    private void displayAmPm()
    {
        if (formatAM)
        {
            amText.text = "AM";
        }
        else
        {
            amText.text = "PM";
        }
    }

    override public bool GetAM()
    {
        return formatAM;
    }

    override public bool Get12hr()
    {
        return format12hr;
    }

}
