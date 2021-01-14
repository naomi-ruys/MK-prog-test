using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    public GameObject hours, minutes, seconds;
    public GameObject separatorHM, separatorMS;
    public GameObject amPM;

    private MyTime currTime = new MyTime(10, 50, 50);
    private bool format12hr = true;
    private bool formatAM = false;
    private bool pause = false;

    private Text hText, mText, sText, amText;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            IncrementTime();
        }

        //display time
        sText.text = DoubleDigit((int)currTime.second);
        mText.text = DoubleDigit(currTime.minute);
        hText.text = DoubleDigit(currTime.hour);

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

    private string DoubleDigit(int time)
    {
        return time.ToString().PadLeft(2, '0');
    }

    private struct MyTime
    {
        public float second;
        public int hour, minute;

        public MyTime (int h, int m, float s)
        {
            hour = h;
            minute = m;
            second = s;
        }
    }

}
