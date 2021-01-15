using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : Clock
{
    public GameObject hours, minutes, seconds;
    public GameObject separatorHM, separatorMS;
    public GameObject amPM;
    //public Button removeButton;

    private Util.MyTime currTime = new Util.MyTime(10, 50, 50);
    private bool format12hr = true;
    private bool formatAM = false;
    private bool pause = false;

    private List<GameObject> setTimeButtons = new List<GameObject>();

    private Text hText, mText, sText, amText;

    void Start()
    {
        //get all text elements
        hText = hours.GetComponentInChildren<Text>();
        mText = minutes.GetComponentInChildren<Text>();
        sText = seconds.GetComponentInChildren<Text>();
        amText = amPM.GetComponentInChildren<Text>();

        //TODO -- Set this in a "SetTime" function based on user input -------------------
        //Set initial am/pm
        if (formatAM)
        {
            amText.text = "AM";
        }
        else
        {
            amText.text = "PM";
        }
    }

    void Update()
    {
        if (!pause) //--------------------------does this need a pause button??
        {
            IncrementTime();
        }

        //display time
        sText.text = Util.DoubleDigit((int)currTime.second);
        mText.text = Util.DoubleDigit(currTime.minute);
        hText.text = Util.DoubleDigit(currTime.hour);

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

    public void SetTime()
    {
        int hr, min;
        float sec = 0;

        pause = true;
        hr = 1;
        min = 1;


        //Set the user given time, as the current time
        currTime = new Util.MyTime(hr, min, sec);
    }

    public void SetTime(int hr, int min, float sec)
    {
        currTime = new Util.MyTime(hr, min, sec);
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
        if (formatAM)
        {
            amText.text = "PM";
        } else
        {
            amText.text = "AM";
        }
        formatAM = !formatAM;
    }

    

}
