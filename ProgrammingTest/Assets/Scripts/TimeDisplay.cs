using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : Clock
{
    [SerializeField] private GameObject hours, minutes, seconds;
    [SerializeField] private GameObject separatorMS, amPM;

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
        currTime = System.DateTime.Now;

        //get the AM/PM of the current time
        string tt = currTime.ToString("tt");

        if (tt == AMString)
        {
            formatAM = true;
        }
        else
        {
            formatAM = false;
        }

        Format12();
        InvokeRepeating(nameof(IncrementTime), 0, 1f);
    }

    void Update()
    {
        //display time
        amText.text = currTime.ToString("tt");
        sText.text = currTime.ToString("ss");
        mText.text = currTime.ToString("mm");

        if (format12hr)
        {
            hText.text = currTime.ToString("hh");
        }
        else
        {
            hText.text = currTime.ToString("HH");
        }
    }

    private void IncrementTime()
    {
        currTime = currTime.AddSeconds(1f);
    }

    public override void SetTime(System.DateTime newTime, bool am = true)
    {
        currTime = newTime;
        formatAM = am;
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
            case 2:         //24hr HH:mm
                RemoveSeconds();
                Format24();
                break;
            case 3:         //24hr HH:mm:ss
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
        amPM.SetActive(false);
        format12hr = false;
    }

    private void Format12()
    {
        amPM.SetActive(true);
        format12hr = true;
    }

    public override int GetHour()
    {
        if (format12hr)
        {
            return int.Parse(currTime.ToString("hh"));
        }
        else
        {
            return int.Parse(currTime.ToString("HH"));
        }
    }

    public override bool GetAM()
    {
        return formatAM;
    }

    public override bool Get12hr()
    {
        return format12hr;
    }

}
